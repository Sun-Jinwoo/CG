using UnityEngine;
using PackagePersona;
using System.Collections.Generic;
using System.IO;
using System;

public class GestorEstudiantes : MonoBehaviour
{
    private List<Estudiante> estudiantes = new List<Estudiante>();
    private List<Estudiante> ingenieros = new List<Estudiante>();
    private List<Estudiante> otros = new List<Estudiante>();
    private string rutaArchivo = Path.Combine(Application.streamingAssetsPath, "Estudiantes.txt");
    private string rutaIngenieros = Path.Combine(Application.streamingAssetsPath, "Ingenieros.json");
    private string rutaOtros = Path.Combine(Application.streamingAssetsPath, "Otros.json");

    void Start()
    {
        // Tarea 1: Leer archivo y crear lista de estudiantes
        LeerArchivoEstudiantes();

        // Tarea 2: Crear e imprimir sublista de estudiantes de ingeniería
        CrearSublistaIngenieros();

        // Tarea 3: Agregar estudiante en una posición específica (índice 3)
        AgregarEstudianteEnPosicion(new Estudiante("21498", "Diseño Industrial", "Alex Johnson", "ajohnson@uao.edu.co", "calle99"), 3);

        // Tarea 4: Agregar estudiante al inicio
        AgregarEstudianteAlInicio(new Estudiante("21499", "Ingenieria Biomedica", "Beta Zeta", "bzeta@uao.edu.co", "avenida1"));

        // Tarea 5: Separar estudiantes en listas de ingeniería y no ingeniería
        SepararEstudiantes();

        // Tarea 6: Guardar las listas en archivos JSON
        GuardarListas();
    }

    void LeerArchivoEstudiantes()
    {
        try
        {
            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');
                    if (datos.Length == 5)
                    {
                        Estudiante e = new Estudiante(datos[3], datos[4], datos[0], datos[1], datos[2]);
                        estudiantes.Add(e);
                        Debug.Log($"Estudiante leído: {e.NameP} ({e.NameCarrera})");
                    }
                }
                Debug.Log($"Cargados {estudiantes.Count} estudiantes.");
            }
            else
            {
                Debug.LogError($"El archivo no existe en: {rutaArchivo}");
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error al leer el archivo: {e.Message}");
        }
    }

    void CrearSublistaIngenieros()
    {
        ingenieros.Clear();
        foreach (Estudiante estudiante in estudiantes)
        {
            if (estudiante.NameCarrera.ToLower().Contains("ingenieria"))
            {
                ingenieros.Add(estudiante);
            }
        }

        Debug.Log("Estudiantes de ingeniería:");
        foreach (Estudiante ing in ingenieros)
        {
            Debug.Log($"- {ing.NameP} ({ing.MailP}, {ing.DirP}, {ing.CodeE}, {ing.NameCarrera})");
        }
    }

    void AgregarEstudianteEnPosicion(Estudiante nuevoEstudiante, int indice)
    {
        if (indice >= 0 && indice <= estudiantes.Count)
        {
            estudiantes.Insert(indice, nuevoEstudiante);
            Debug.Log($"Agregado {nuevoEstudiante.NameP} en la posición {indice}.");
        }
        else
        {
            Debug.LogError($"Índice inválido {indice} para agregar estudiante.");
        }
    }

    void AgregarEstudianteAlInicio(Estudiante nuevoEstudiante)
    {
        estudiantes.Insert(0, nuevoEstudiante);
        Debug.Log($"Agregado {nuevoEstudiante.NameP} al inicio.");
    }

    void SepararEstudiantes()
    {
        ingenieros.Clear();
        otros.Clear();
        foreach (Estudiante estudiante in estudiantes)
        {
            if (estudiante.NameCarrera.ToLower().Contains("ingenieria"))
            {
                ingenieros.Add(estudiante);
            }
            else
            {
                otros.Add(estudiante);
            }
        }

        Debug.Log("Listas separadas:");
        Debug.Log($"Ingenieros ({ingenieros.Count}):");
        foreach (Estudiante ing in ingenieros)
        {
            Debug.Log($"- {ing.NameP} ({ing.NameCarrera})");
        }
        Debug.Log($"Otros ({otros.Count}):");
        foreach (Estudiante otro in otros)
        {
            Debug.Log($"- {otro.NameP} ({otro.NameCarrera})");
        }
    }

    void GuardarListas()
    {
        try
        {
            // Guardar lista de ingenieros
            bool exitoIngenieros = Utilidades.SaveDataStudent(ingenieros, rutaIngenieros);
            if (exitoIngenieros)
            {
                Debug.Log($"Guardados {ingenieros.Count} ingenieros en {rutaIngenieros}");
            }
            else
            {
                Debug.LogError("Fallo al guardar la lista de ingenieros.");
            }

            // Guardar lista de otros
            bool exitoOtros = Utilidades.SaveDataStudent(otros, rutaOtros);
            if (exitoOtros)
            {
                Debug.Log($"Guardados {otros.Count} otros en {rutaOtros}");
            }
            else
            {
                Debug.LogError("Fallo al guardar la lista de otros.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al guardar archivos: {e.Message}");
        }
    }
}