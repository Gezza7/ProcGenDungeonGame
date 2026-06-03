using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    string path;
    StreamWriter writer;
    

    public void setPath(string num)
    {
        File.Create(Application.dataPath + "/Data/" + num);
        path = Application.dataPath + "/Data/" +num +"/Data.txt";
        writer = new StreamWriter(path);
    }

    public void saveTimes(float total, float time1, float time2, float time3, float time4, float[] times)
    {
        writer.Write("---TIME DATA---");
        writer.Write("Total Time " +total);
        writer.Write("Section 1 Total Time " + time1);
        writer.Write("Section 2 Total Time " + time2);
        writer.Write("Section 3 Total Time " + time3);
        writer.Write("Section 4 Total Time " + time4);
        writer.Write("Section 1 first Time " + times[0] + "Section 2 first Time " + times[1] + "Section 3 first Time " + times[2] + "Section 4 first Time " + times[3]);
    }

    public void savelight(float average, float max, float min)
    {
        writer.Write("---LIGHT DATA---");
        writer.Write("Average Light Intensity " + average);
        writer.Write("Max Light Intensity " + max);
        writer.Write("Min Light Intensity " + min);
    }

    public void heatMapScreenShots()
    {
        //Camera.main.targetDisplay
    }

}
