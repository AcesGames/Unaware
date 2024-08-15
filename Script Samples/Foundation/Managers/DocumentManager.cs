using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class DocumentManager : MonoBehaviour
{
    [SerializeField] private List<Document> _documents = new();

    private int _currentDocumentID;
    public int CurrentDocumentID => _currentDocumentID;

    public void SetCurrentDocument(int id)=> _currentDocumentID = id;

    public void ChangeDocumentProgress(int id, Document.DocumentProgress progress)
    {
        for (int i = 0; i < _documents.Count; i++)
        {
            if (_documents[i].Data.Id == id)
            {
                _documents[i].SetDocument(_documents[i].Data, progress);
                break;
            }
        }
    }

    public Document GetDocument(int id)
    {
        for (int i = 0; i < _documents.Count; i++)
        {
            if (_documents[i].Data.Id == id)
                return _documents[i];
        }

        return null;
    }

    public List<Document> GetAllDocuments()
    {
        return _documents;
    }

    public List<Document> GetActiveDocumnents()
    {
        List<Document> activeDocuments = new();

        for (int i = 0; i < _documents.Count; i++)
        {
            if (_documents[i].Progress == Document.DocumentProgress.FOUND)
            {
                activeDocuments.Add(_documents[i]);
            }
        }

        return activeDocuments;
    }

    public bool IsDocumentFound(int id)
    {
        for (int i = 0; i < _documents.Count; i++)
        {
            if (_documents[i].Data.Id == id && _documents[i].Progress == Document.DocumentProgress.FOUND)
                return true;
        }

        return false;
    }

#if UNITY_EDITOR
    public void CacheAllDocuments(string path)
    {
        _documents.Clear();

        string[] files = Directory.GetFiles(path, "*.asset", SearchOption.AllDirectories);

        foreach (var assetPath in files)
        {

            Document document = new Document();
            DocumentData data = AssetDatabase.LoadAssetAtPath(assetPath.ToString(), typeof(DocumentData)) as DocumentData;

            document.SetDocument(data, Document.DocumentProgress.NOTFOUND);

            _documents.Add(document);
        }

        EditorUtility.SetDirty(this);
    }
#endif

    [Button]
    private void AddAllDocuments()
    {
        foreach (var document in _documents)
        {
            document.SetDocument(document.Data, Document.DocumentProgress.FOUND);
        }
    }
}
