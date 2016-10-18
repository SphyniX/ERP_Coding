/**
 * 文  件  名：GPSMgr 
 * 作       者：浪浪
 * 生成日期：16-08-11 07:15:10"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using System.Collections;

public class GPSMgr : MonoSingleton<GPSMgr>
{

    private string gps_info = "";

    // Input.location = LocationService
    // LocationService.lastData = LocationInfo 
    void Start() {

    }
    
    public void StartGps() {
        LogMgr.D("start gps");
#if UNITY_EDITOR || UNITY_STANDALONE
        StartCoroutine(IEStartGPS());
#else
        StartCoroutine(IEStartGPS());
#endif
    }
    public string GetGps() {
        //this.gps_info = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        //this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;
        this.gps_info = Input.location.lastData.latitude + "," + Input.location.lastData.longitude;
        LogMgr.D(this.gps_info);
#if UNITY_EDITOR || UNITY_STANDALONE
        return "31.16631,121.4034";
#else
        return gps_info;
#endif

    }
    public void StopGps()
    {
        Input.location.Stop();
    }
    IEnumerator IEStartGPS()
    {
        // Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置
        // LocationService.isEnabledByUser 用户设置里的定位服务是否启用
        if (!Input.location.isEnabledByUser) {
            this.gps_info = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";
            LogMgr.D(this.gps_info);
            yield return false;
        }

        // LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用
        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            // 暂停协同程序的执行(1秒)
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1) {
            this.gps_info = "Init GPS service time out";
            LogMgr.D(this.gps_info);
            yield return false;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            this.gps_info = "Unable to determine device location";
            LogMgr.D(this.gps_info);
            yield return false;
        } else {
            this.gps_info = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
            LogMgr.D(this.gps_info);
            yield return new WaitForSeconds(100);
        }
    }
}
