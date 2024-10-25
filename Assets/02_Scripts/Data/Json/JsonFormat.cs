using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JSON ���Ŀ� ���� Ŭ����. Google Sheets API���� ���� �����͸� �� �������� �Ľ�.
/// </summary>
public class JsonFormat
{
    public string range;                // ��û�� ������ ���� (��: "WaveData!A1:G7").    
    public string majorDimension;       // �������� �ֿ� ���� (ROWS �Ǵ� COLUMNS).
    public string[][] values;           // ���� �����͸� �����ϴ� 2���� �迭. �� ���� �迭�� ǥ����.
}
