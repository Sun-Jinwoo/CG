using UnityEngine;
using System;
using System.Collections.Generic;
using PackagePersona;
using System.IO;

public static class Utilidades
{
    public static bool SaveDataStudent(List<Estudiante> listaE, string filePath)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(new EstudianteListWrapper { estudiantes = listaE }, true);
            string folderPath = Path.GetDirectoryName(filePath);

            // Crear la carpeta si no existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Escribir el archivo
            File.WriteAllText(filePath, jsonString);

            Debug.Log($"Archivo JSON guardado correctamente en: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al guardar archivo JSON: {ex.Message}");
            return false;
        }
    }

    public static bool SaveDataPuntos(List<Punto2D> listaP, string filePath)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(new PuntosListWrapper { puntos = listaP }, true);
            string folderPath = Path.GetDirectoryName(filePath);

            // Crear la carpeta si no existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Escribir el archivo
            File.WriteAllText(filePath, jsonString);

            Debug.Log($"Archivo JSON guardado correctamente en: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al guardar archivo JSON: {ex.Message}");
            return false;
        }
    }

    internal static bool SaveDataStudent(List<Estudiante> listaE)
    {
        throw new NotImplementedException();
    }

    internal static void SaveDataPuntos(List<Punto2D> puntos)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class EstudianteListWrapper
{
    public List<Estudiante> estudiantes;
}

[Serializable]
public class PuntosListWrapper
{
    public List<Punto2D> puntos;
}