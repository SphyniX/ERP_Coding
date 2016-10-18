using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace vietlabs {
	public class LIInfo {
		public bool CanDelete; //show or hide Delete button
		public bool CanDrag; //allow drag this item or not
		public bool? Selected; //null = disable selection
		internal Rect _cRect;
		internal Rect _tRect;

		internal bool IsAnimate { get; private set; }
		internal Rect CurrentRect { get { return _cRect; } }

		private LIInfo CheckAnimate {
			get {
				IsAnimate = _tRect.xIsDifferent(_cRect);
				if (!IsAnimate) { _cRect = _tRect; }
				return this;
			}
		}

		internal LIInfo SetX(float value) {
			_cRect.x = value;
			_tRect.x = value;
			return CheckAnimate;
		}

		internal LIInfo SetY(float value) {
			_cRect.y = value;
			_tRect.y = value;
			return CheckAnimate;
		}

		internal LIInfo SetHeight(float value) {
			_cRect.height = value;
			_tRect.height = value;
			return CheckAnimate;
		}

		internal LIInfo SetWidth(float value) {
			_cRect.width = value;
			_tRect.width = value;
			return CheckAnimate;
		}

		internal LIInfo SetXY(float x, float y) {
			SetX(x);
			SetY(y);
			return CheckAnimate;
		}

		internal LIInfo TweenX(float value) {
			_tRect.x = value;
			return CheckAnimate;
		}

		internal LIInfo TweenY(float value) {
			_tRect.y = value;
			return CheckAnimate;
		}

		internal LIInfo TweenDY(float value) {
			_tRect.y += value;
			return CheckAnimate;
		}

		internal LIInfo TweenH(float value) {
			_tRect.height = value;
			return CheckAnimate;
		}

		internal Rect AnimateStep() {
			if (!IsAnimate || Event.current.type != EventType.repaint) { return _cRect; }
			_cRect = _cRect.xLerp(_tRect, 0.1f);
			return CheckAnimate.CurrentRect;
		}
	}

	public class GUIListTheme {
		public float CellHeight = 20f;
		public bool Draggable = true;
		public Texture2D DraggingTex;
		public Texture2D EvenBgTex;
		//public float MaxHeight = 200f;
		public Texture2D OddBgTex;

		public bool Selectable = false;
		public Texture2D SelectedTex;
		public bool ShowDelete = false;
		public bool ShowIndex = false;

		public Rect RectOffset;
	}

	internal class DragData {
		internal int DragIdx = -1;
		internal float DragMouseX;
		internal float DragMouseY;
		internal int DropIdx = -1;

		internal int Idx; //same as DragIdx if isDragging
		internal LIInfo Info;
		internal object Target;

		public bool IsDragging { get { return DragIdx != -1; } }
		public bool IsDropping { get { return DropIdx != -1 && DragIdx == -1; } }
	}


	public class vlbGUIList<T> where T : class {
		//static cache : only use when reorder components to prevent refresh-flickering
		//private static Object _c_idx;
		//private static Rect _c_GuiRect;
		//private static Rect _c_ScrollRect;
		//private static Vector2 _c_ScrollPosition;
		//private static int[] _c_SelectedIndexes;

		public List<T> CacheList;

		public bool NeedRepaint;
		public GUIListTheme Theme;
		internal Dictionary<T, LIInfo> RectMap;

		//temporary vars
		//TODO : cache selection
		internal DragData Drag;
		internal Rect GuiRect;
		internal Object Id;
		internal Vector2 MousePos;
		internal Vector2 ScrollPosition;
		internal Rect ScrollRect;

		internal vlbGUIList(List<T> list, GUIListTheme theme = null, Object listId = null) {
			Drag = new DragData();
			RectMap = new Dictionary<T, LIInfo>();
			CacheList = list;

			Theme = theme ?? new GUIListTheme {
				DraggingTex		= Color.blue.xGetTexture2D(),
				SelectedTex		= Color.yellow.xGetTexture2D(),
				OddBgTex		= Color.blue.xGetTexture2D(),
				EvenBgTex		= Color.gray.xGetTexture2D(),
				RectOffset		= new Rect(0f, 0, 0f, 0)
			}; 

			/*if (_c_idx == listId) {
				GuiRect = _c_GuiRect;
				ScrollRect = _c_ScrollRect;
				ScrollPosition = _c_ScrollPosition;

				if (_c_SelectedIndexes != null) {
					foreach (var idx in _c_SelectedIndexes) {
						if (idx > CacheList.Count) {
							Debug.LogWarning("Cache erorr - trying to select <" + idx + "> in a list of " + CacheList.Count + " items - might be conflicting ids");
							continue;
						}

						var info = GetInfo(CacheList[idx]);
						info.Selected = true;
					}
				}
			}*/

			Id = listId;
		}

		public List<T> SelectedList { get { return CacheList.Where(item => GetInfo(item).Selected == true).ToList(); } }

		//-------------------------------------- INTERNAL UTILS -------------------------------------------

		private Rect MouseDragRect {
			get {
				return new Rect(Event.current.mousePosition.x - Drag.DragMouseX,
								Event.current.mousePosition.y - Drag.DragMouseY,
								GuiRect.width,
								Drag.Info.CurrentRect.height);
			}
		}

	    public Rect GetGUILayoutViewRect() {
            GUILayoutUtility.GetRect(new GUIContent(" "), EditorStyles.miniLabel);
            var rect = GUILayoutUtility.GetLastRect();
            GUILayout.FlexibleSpace();
            var lastRect2 = GUILayoutUtility.GetLastRect();
            rect.height += lastRect2.height;
	        return rect;
	    }

		internal void Draw(Func<Rect, T, int, int> onDrawCell,
		Action<int, int, T> onReorder = null,
		Action<T, int> OnRightClick = null,
        Action<int, int, T> onBeforeReorder = null,
        Rect? drawRect = null) {

			NeedRepaint = false;

			const float scrollW = 16f;
		    var rect        = drawRect ?? GetGUILayoutViewRect();
			var hasScroll	= (rect.height>2f) && (GuiRect.height > rect.height);
			var counter		= 0;
			var ch			= 0f;

			if (rect.width > 1f) {
                rect            = rect.xAdd(Theme.RectOffset);

				GuiRect.x		= rect.x;
				GuiRect.y		= rect.y;
				GuiRect.width	= rect.width;
			}

			var x = GuiRect.x;
			var y = GuiRect.y;
			var w = GuiRect.width;
			var h = GuiRect.height;

			MousePos = Event.current.mousePosition;

			if (hasScroll) {
				ScrollRect = GuiRect.h(rect.height);
				ScrollPosition = GUI.BeginScrollView(ScrollRect, ScrollPosition, GuiRect.dw(-scrollW), false, true);
			}

			if (Drag.IsDropping) { HandleDrop(onReorder, onBeforeReorder); } else if (Drag.IsDragging) { HandleDrag(); }
			if (CacheList == null) return;

			for (int i = 0; i < CacheList.Count; i++) { //layout
				T item = CacheList[i];
				var info = GetInfo(item).SetWidth(w - (hasScroll ? scrollW : 0)).SetX(x);
				var first = info.CurrentRect.height == 0;

				if (first) {
					info.SetY(y);//.SetHeight(Theme.CellHeight);
				}
				info.AnimateStep();

				if (!Drag.IsDragging) {
					if (Drag.IsDropping && counter == Drag.DropIdx) {
						if (item != Drag.Target) {
							LIInfo animx = Drag.Info;
							animx.TweenY(y + ch);
							ch += animx.CurrentRect.height;
						}
					}

					if (item != Drag.Target) {
						if (Event.current.type == EventType.layout) { info.SetY(y + ch); } else { info.TweenY(y + ch); }
					}
				}

				if (item == Drag.Target) { continue; }
				int newH = DrawCell(i != Drag.DragIdx ? onDrawCell : null,
									info.CurrentRect,
									item,
									(Drag.Target != null && counter >= Drag.DropIdx) ? counter + 1 : counter); //!(info.Selected==true)
				if (newH > 0) {
					if (first) {
						info.SetHeight(newH);
					} else {
						info.TweenH(newH);
					}
				}

				if (info.IsAnimate && !NeedRepaint) { NeedRepaint = true; }

				if (!Drag.IsDragging && !Drag.IsDropping) {
                    if (info.CanDrag && info.CurrentRect.w(30f).h(20f).xLMB_isDown().noModifier) {
                        StartDrag(i);
                    } else if (info.Selected != null && info.CurrentRect.xLMB_isDown().noModifier) {
                        info.Selected = !info.Selected;
                    }

					if (OnRightClick != null && info.CurrentRect.xRMB_isDown().noModifier) { OnRightClick(item, i); }
				}

				ch += info.CurrentRect.height;
				counter++;
			}

			if (Drag.IsDragging) { ch += Drag.Info.CurrentRect.height; }

			if (ch != h && ch > 0) {
				GuiRect.height = ch;
				NeedRepaint = true;
			}

			if (Drag.Target != null) {
				LIInfo dragRect = Drag.Info;
				int newH = DrawCell(onDrawCell,
									Drag.IsDragging ? MouseDragRect : dragRect.CurrentRect,
									(T) Drag.Target,
									Drag.DropIdx);
				if (newH > 0) { dragRect.SetHeight(newH); }
			}

			if (hasScroll) { GUI.EndScrollView(); }
		}

		//---------------------------------------- PUBLIC APIs --------------------------------------------

		public LIInfo GetInfo(T item) {
			if (item == null) {
				Debug.LogWarning("item should not be null");
				return null;
			}

			if (RectMap.ContainsKey(item)) { return RectMap[item]; }

			LIInfo info = new LIInfo 
			{
				Selected = Theme.Selectable ? (bool?) false : null,
				CanDrag = Theme.Draggable,
				CanDelete = Theme.ShowDelete
			};//.SetXY(GuiRect.x, GuiRect.y);.SetHeight(Theme.CellHeight);

			RectMap.Add(item, info);
			return info;
		}

		public void Focus(T item) {
			var idx = CacheList.IndexOf(item);
			if (idx == -1) return;

			var info = GetInfo(item);
			var vh = ScrollRect.height;
			var y	= info._cRect.y;
			var min = GuiRect.y + ScrollPosition.y;
			var max = min + vh;

			if (min <= y && y <= max) return; //already in view : do nothing
			ScrollPosition.y = Mathf.Max( y-GuiRect.y - vh/2, 0f);
		}

		public void StartDrag(int dragIdx) {
			Drag.Target = CacheList[dragIdx]; //List.First(item => item.Index == dragIdx);
			Drag.Info = GetInfo((T) Drag.Target);
			Drag.DragIdx = dragIdx;
			Drag.DropIdx = dragIdx;
			Drag.Idx = dragIdx;

			Vector2 m = Event.current.mousePosition;
			Rect r = Drag.Info.CurrentRect;
			Drag.DragMouseX = m.x - r.x;
			Drag.DragMouseY = m.y - r.y;

            EditorGUI.FocusTextInControl(null);
		}
		public void StopDrag(bool drop) {
			if (!drop) { Drag.DropIdx = Drag.DragIdx; }
			Drag.DragIdx = -1;
			Rect mRect = MouseDragRect;
			Drag.Info.SetXY(mRect.x, mRect.y).TweenX(GuiRect.x);
		}
		public bool IsSelected(T item) { return GetInfo(item).Selected == true; }
		public void SetSelected(T item, bool value) { GetInfo(item).Selected = value; }
		public void InvertSelection() {
			foreach (T item in CacheList) {
				LIInfo info = GetInfo(item);
				info.Selected = !info.Selected;
			}
		}
		public void SetSelection(bool isSelected) {
			foreach (T item in CacheList) {
				LIInfo info = GetInfo(item);
				info.Selected = isSelected;
			}
		}
		/*public void CacheRect() {
			if (Id != null) {
				_c_idx = Id;
				_c_GuiRect = GuiRect;
				_c_ScrollRect = ScrollRect;
				_c_ScrollPosition = ScrollPosition;

				var list = new List<int>();
				for (var i = 0; i < CacheList.Count; i++) {
					var info = GetInfo(CacheList[i]);
					if (info.Selected == true) list.Add(i);
				}

				_c_SelectedIndexes = list.ToArray();
			}
		}*/
		private void HandleDrag() {
			NeedRepaint = true;

			if (Event.current.type == EventType.mouseDrag) { //only update when mouse drag
				LIInfo dragAnim = Drag.Info;

				var ch = GuiRect.y;
				var dropIdx = -1;
				var lastCheck = false;
				var cnt = 0;
				var localMouse = Event.current.mousePosition;

				foreach (T item in CacheList) {
					var info = GetInfo(item);

					if (item == Drag.Target) { continue; }

					var checkRect = info.CurrentRect.y(ch);
					var check = checkRect.Contains(localMouse);
					if (lastCheck && !check) { dropIdx = cnt - 1; }
					lastCheck = check;

					info.TweenY(ch);
					ch += info.CurrentRect.height;
					cnt++;
				}

				GuiRect.height = ch + dragAnim.CurrentRect.height - GuiRect.y;
				if (dropIdx == -1) { dropIdx = CacheList.Count - (lastCheck ? 2 : 1); }
				Drag.DropIdx = dropIdx;

				cnt = 0;
				foreach (T t in CacheList) {
					if (t == Drag.Target) { continue; }

					if (cnt >= dropIdx) {
						GetInfo(t).TweenDY(dragAnim.CurrentRect.height);
						//Debug.Log(i + "===>" + cnt + "--->");
					}
					cnt++;
				}
			}

			//stop drag if mouse out or up
			if (Drag.IsDragging && ScrollRect.height > 0 && !ScrollRect.Contains(MousePos)) {
				//Debug.Log("Stop Drag at : " + MousePos + ":" + GuiRect + ":" + ScrollRect);
				StopDrag(false);
			}
			if (Event.current.type == EventType.MouseUp) { StopDrag(true); }
		}
		private void HandleDrop(Action<int, int, T> onReorder, Action<int, int, T> onBeforeReorder) {
			NeedRepaint = true;
			LIInfo dragRect = Drag.Info;
			dragRect.AnimateStep();
			if (dragRect.IsAnimate) {
				return; //check if drop completed
			}

			int dragIdx = Drag.Idx;
			int dropIdx = Drag.DropIdx;

            if (onBeforeReorder != null) { onBeforeReorder(dragIdx, dropIdx, (T)Drag.Target); }
			CacheList.Remove((T) Drag.Target);
			CacheList.Insert(dropIdx, (T) Drag.Target);
            if (onReorder != null) { onReorder(dragIdx, dropIdx, (T)Drag.Target); }

			Drag.DropIdx = -1;
			Drag.Target = null;
		}
		private int DrawCell(Func<Rect, T, int, int> onDrawCell, Rect r, T item, int idx = -1) {
			LIInfo info = GetInfo(item);

			if (item == Drag.Target && onDrawCell != null) {
				if (Theme.DraggingTex != null) { GUI.DrawTexture(r.h(info.CurrentRect.height), Theme.DraggingTex); }
                if (Theme.ShowIndex) GUI.Label(r.dx(3).dy(3).w(30f), (idx + 1) + "");
			} else {
				if (info.Selected == true) {
					if (Theme.SelectedTex != null) { GUI.DrawTexture(r.dl(1).dt(1), Theme.SelectedTex); }
				} else if (idx%2 == 0) { //even
					if (Theme.EvenBgTex != null) { GUI.DrawTexture(r, Theme.EvenBgTex); }
				} else { //odd
					if (Theme.OddBgTex != null) { GUI.DrawTexture(r, Theme.OddBgTex); }
				}
			}

			if (onDrawCell != null) {
                if (Theme.ShowIndex) GUI.Label(r.dx(3).dy(3).w(30f), (idx + 1) + "");
				return onDrawCell(r.dl(Theme.ShowIndex ? 15 : 0), item, idx);
			}

			return -1;
		}
	}
}