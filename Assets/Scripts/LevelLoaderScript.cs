using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelLoaderScript : MonoBehaviour
{
    private string _levelsPath = "/Resources/Levels";

    [SerializeField] private GameObject _grassBlock;
    [SerializeField] private TileBase _singleWallTile;
    [SerializeField] private GameObject _outerWallBlock;
    [SerializeField] private GameObject _crateBlock;
    [SerializeField] private GameObject _storageBlock;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _parent;
    [SerializeField] private InputManager _inputManager;

    [SerializeField] private Tilemap _wallsTilemap;
    [SerializeField] private TileBase _wallTileBase;

    [SerializeField] private Tilemap _groundTilemap;
    [SerializeField] private TileBase _groundTileBase;

    [SerializeField] private GameObject _gridShader;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private GameObject _player;

    List<List<char>> _arr;

    private void Awake()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Levels", $"levels{GameModifiers.levelNumber}.txt");

        if (!File.Exists(filePath))
        {
            Debug.Log("level not found");
            SceneManager.LoadScene(0);
            return;
        }

        /*
        DirectoryInfo dir = new DirectoryInfo(_levelsPath);
        var files = dir.GetFiles("*txt");
        if (files.Length == 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        if (GameModifiers.levelNumber >= files.Length)
        {
            SceneManager.LoadScene(0);
            return;
        }
        */

        _arr = GetLevelArray(filePath);
        
        InstantiateLevel();
        

    }
    private void Start()
    {
        CommandInvoker.ClearStack();
    }

    private List<List<char>> GetLevelArray(string path)
    {
        var level = new List<List<char>>();

        try
        {
            StreamReader sr = new(path);

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

    private void InstantiateLevel()
    {
        var arr = _arr;
        _wallsTilemap.FloodFill(new Vector3Int(arr.Count, arr.OrderByDescending(x => x.Count).First().Count), _wallTileBase);

        for (int i = 0; i < arr.Count; i++)
        {
            for(int j = 0; j < arr[i].Count; j++)
            {
                switch (arr[i][j])
                {
                    case '.':
                        _groundTilemap.SetTile(new Vector3Int(i, j), _groundTileBase);
                        _wallsTilemap.SetTile(new Vector3Int(i, j), null);
                        break;
                    case 'x':
                        _player = Instantiate(_playerPrefab, new Vector2(i, j), new Quaternion(), _parent.transform);
                        _inputManager._player = _player.GetComponent<PlayerMover>();
                        _groundTilemap.SetTile(new Vector3Int(i, j), _groundTileBase);
                        _wallsTilemap.SetTile(new Vector3Int(i, j), null);
                        break;
                    case '#':
                        _wallsTilemap.SetTile(new Vector3Int(i,j), _singleWallTile);
                        _groundTilemap.SetTile(new Vector3Int(i,j), _groundTileBase);
                        break;
                        
                    case '*':
                        Instantiate(_crateBlock, new Vector2(i, j), new Quaternion(), _parent.transform);
                        _groundTilemap.SetTile(new Vector3Int(i, j), _groundTileBase);
                        _wallsTilemap.SetTile(new Vector3Int(i, j), null);
                        break;
                    
                    case 'o':
                        Instantiate(_storageBlock, new Vector2(i, j), new Quaternion(), _parent.transform);
                        _wallsTilemap.SetTile(new Vector3Int(i, j), null);
                        break;
                    
                    default:
                        _wallsTilemap.SetTile(new Vector3Int(i, j), _wallTileBase);
                        break;
                }
            }
        }

        var shPos = _gridShader.transform;
        shPos.position = new Vector2(0f,0f);
        shPos.localScale = new Vector3(501, 501, 501);

        _camera.Follow = _player.transform;
    }
}
