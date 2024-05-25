using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoaderScript : MonoBehaviour
{
    private string _levelsPath = "Assets/Levels/levels.txt";
    [SerializeField] private int _levelToLoad = 0;
    private bool _recordLevel = false;

    [SerializeField] private GameObject _grassBlock;
    [SerializeField] private GameObject _wallBlock;
    [SerializeField] private GameObject _outerWallBlock;
    [SerializeField] private GameObject _crateBlock;
    [SerializeField] private GameObject _storageBlock;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _parent;
    [SerializeField] private InputManager _inputManager;

    [SerializeField] private GameObject _gridShader;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        var arr = GetLevelArray(_levelToLoad);
        InstantiateLevel(arr);
    }

    private List<List<char>> GetLevelArray(int _levelToLoad)
    {
        var level = new List<List<char>>();

        try
        {
            StreamReader sr = new(_levelsPath);

            var line = sr.ReadLine();
            while(line != null)
            {
                
                level.Add(new());

                foreach( char c in line)
                {
                    level[level.Count - 1].Add(c);
                }

                line = sr.ReadLine();
            }
            sr.Close();
        }
        catch(Exception ex)
        {
            Debug.Log("ex: " + ex.Message);
        }

        return level;
    }

    private void InstantiateLevel(List<List<char>> arr)
    {

        for(int i = 0; i < arr.Count; i++)
        {
            for(int j = 0; j < arr[i].Count; j++)
            {
                Instantiate(_grassBlock, new Vector2(j, arr[i].Count - i),new Quaternion(),_parent.transform);
                switch (arr[i][j])
                {
                    case 'x':
                        GameObject pl = Instantiate(_playerPrefab, new Vector2(j, arr[i].Count - i), new Quaternion(), _parent.transform);
                        _inputManager._player = pl.GetComponent<PlayerMover>();
                        break;
                    case '#':
                        Instantiate(_wallBlock, new Vector2(j, arr[i].Count - i), new Quaternion(), _parent.transform);
                        break;
                    case '*':
                        Instantiate(_crateBlock, new Vector2(j, arr[i].Count - i), new Quaternion(), _parent.transform);
                        break;
                    case 'o':
                        Instantiate(_storageBlock, new Vector2(j, arr[i].Count - i), new Quaternion(), _parent.transform);
                        break;
                    default: 
                        break;
                }
            }
        }

        for(int i = 0; i <= arr.Count +1; i++)
        {
            Instantiate(_outerWallBlock, new Vector2(-1, i), new Quaternion(), _parent.transform);
        }
        for (int i = 0; i <= arr.Count + 1; i++)
        {
            Instantiate(_outerWallBlock, new Vector2(arr[0].Count, i), new Quaternion(), _parent.transform);
        }
        for (int i = 0; i <= arr.Count; i++)
        {
            Instantiate(_outerWallBlock, new Vector2(i, 0), new Quaternion(), _parent.transform);
        }
        for (int i = 0; i <= arr.Count; i++)
        {
            Instantiate(_outerWallBlock, new Vector2(i, arr.Count + 1), new Quaternion(), _parent.transform);
        }

        var shPos = _gridShader.transform;
        if(arr.Count % 2 == 0)
            shPos.position = new Vector2(arr[0].Count/2 - 0.5f, arr.Count / 2 + 0.5f);
        else
        {
            shPos.position = new Vector2(arr[0].Count / 2, arr.Count / 2 + 1 );
        }
        shPos.localScale = new Vector3(arr[0].Count + 2, arr.Count + 2, arr.Count + 2);

        _camera.transform.position = new Vector3(shPos.position.x,shPos.position.y,-1);
        _camera.orthographicSize = arr.Count/2 + 1;
    }
}
