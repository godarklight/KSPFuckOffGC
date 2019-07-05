using System;
using UnityEngine;
namespace KSPFuckOffGC
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class KSPFuckOffGC : MonoBehaviour
    {
        private long lastGCMemory = 0;
        private double lastGCTime = 0d;
        private long gcPeak = 0;
        private const long GC_PRESSURE = 1024 * 1024 * 20;
        private const double GC_INTERVAL = 1d;
        public KSPFuckOffGC()
        {
            DontDestroyOnLoad(this);
        }

        public void Update()
        {
            long currentMemory = GC.GetTotalMemory(false);
            if (currentMemory < lastGCMemory)
            {
                double currentTime = Time.realtimeSinceStartup;
                //We collected!
                if ((currentTime - lastGCTime) < GC_INTERVAL)
                {
                    Debug.Log("Adding pressure, peak: " + gcPeak);
                    byte[] newByteArray = new byte[gcPeak - currentMemory + GC_PRESSURE];
                }
                Debug.Log("GC time: " + (currentTime - lastGCTime) + " seconds");
                lastGCTime = currentTime;
            }
            if (currentMemory > gcPeak)
            {
                gcPeak = currentMemory;
            }
            lastGCMemory = currentMemory;
        }
    }
}
