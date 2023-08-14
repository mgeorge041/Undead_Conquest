using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader 
{
    private StreamReader streamReader;
    public bool EndOfFile { get { 
            if (streamReader != null)
                return streamReader.EndOfStream;
            return true;
        } }
    public Dictionary<int, string> headers;


    // Constructor
    public CSVReader() { }
    
    public CSVReader(string csvFilepath) 
    {
        Read(csvFilepath);
    }


    // Set headers
    private void SetHeaders()
    {
        headers = new Dictionary<int, string>();
        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            string[] fields = line.Split(',');
            for (int i = 0; i < fields.Length; i++)
            {
                headers[i] = fields[i];
            }
            break;
        }
    }


    // Read
    public void Read(string csvFilepath)
    {
        streamReader = new StreamReader(csvFilepath);
        SetHeaders();
    }


    // Get line
    public Dictionary<string, string> GetLine()
    {
        Dictionary<string, string> lineFields = new Dictionary<string, string>();
        int fieldIndex = 0;
        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            string[] fields = line.Split(',');
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];
                string outField = field;
                if (field[0] == '"')
                {
                    for (int j = i + 1; j < fields.Length; j++)
                    {
                        string innerField = fields[j];
                        if (innerField[innerField.Length - 1] == '"') 
                        {
                            for (int k = i + 1; k <= j; k++)
                            {
                                outField += "," + fields[k];
                                i = k;
                            }
                            break;
                        }
                    }
                }
                outField = outField.TrimStart('"');
                outField = outField.TrimEnd('"');
                lineFields[headers[fieldIndex]] = outField;
                Debug.Log(headers[fieldIndex]);
                Debug.Log(outField);
                fieldIndex++;
            }
            break;
        }
        if (streamReader.EndOfStream)
        {
            streamReader.Close();
            streamReader = null;
        }
        return lineFields;
    }
}
