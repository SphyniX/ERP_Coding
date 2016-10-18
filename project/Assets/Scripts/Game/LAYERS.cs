using UnityEngine;
/// <summary>
/// 定义Unity GameObject Layer的mask值
/// </summary>
public static class LAYERS
{
	public static int iDefault = LayerMask.NameToLayer("Default");
    public static int iUI = LayerMask.NameToLayer("UI");
    public static int iGround = LayerMask.NameToLayer("Ground");
	public static int iObstacle = LayerMask.NameToLayer("Obstacle");
	public static int iRole = LayerMask.NameToLayer("Role");
	public static int iFocus = LayerMask.NameToLayer("Focus");
	public static int iCameraFX = LayerMask.NameToLayer("CameraFX");
	public static int iInvisible = LayerMask.NameToLayer("Invisible");

	public static int Default = 1 << iDefault;
    public static int UI = 1 << iUI;
    public static int Ground = 1 << iGround;
	public static int Obstacle = 1 << iObstacle;
	public static int Role = 1 << iRole;
	public static int Focus = 1 << iFocus;
	public static int CameraFX = 1 << iCameraFX;
	public static int Invisible = 1 << iInvisible;
}
