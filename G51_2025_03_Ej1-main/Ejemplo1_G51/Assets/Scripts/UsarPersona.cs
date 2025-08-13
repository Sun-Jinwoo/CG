using UnityEngine;
using PackagePersona;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class UsarPersona : MonoBehaviour
{

    List<Estudiante> listaE = new List<Estudiante>();
    public TMP_InputField nameStudent;
    public TMP_InputField mailStudent;
    public TMP_InputField dirStudent;
    public TMP_InputField CodeStudent;
    public TMP_InputField carreraStudent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {

        loadDataEstudiantes();
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void loadDataEstudiantes()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Estudiantes.txt");
        string fileContent = "";

        if (File.Exists(filePath))
        {
            try
            {
                fileContent = File.ReadAllText(filePath);
                Debug.Log("Contenido del archivo: " + fileContent);
               
                StringReader reader = new StringReader(fileContent);
                
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                                      
                    string[] lineaEstudiante = line.Split(",");
                    Estudiante e = new Estudiante(lineaEstudiante[3], lineaEstudiante[4],
                        lineaEstudiante[0], lineaEstudiante[1], lineaEstudiante[2]);

                    Debug.Log("Persona leida "+e.NameP+" "+e.NameCarrera);
                    listaE.Add(e);
                    
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al leer el archivo: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("El archivo no existe en: " + filePath);
        }
    }
    public void AddStudentList()
    {
        string nameStudent1= nameStudent.text;
        string mailStudent1= mailStudent.text;
        string dirStudent1= dirStudent.text;
        string codeStudent1=CodeStudent.text;
        string carreraS1= carreraStudent.text;
        Estudiante e1 = new Estudiante(codeStudent1, carreraS1,
           nameStudent1, mailStudent1, dirStudent1);

        listaE.Add(e1);

    }

    public void ShowStudentList()
    {
        for (int i = 0; i < listaE.Count; i++)
        {
            Debug.Log(listaE[i].NameP + " " + listaE[i].NameCarrera);
        }

        bool v = Utilidades.SaveDataStudent(listaE);
    }

}