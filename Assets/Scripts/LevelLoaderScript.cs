using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoaderScript : MonoBehaviour
{
    private string _levelsPath = "Assets/Levels/levels.txt";
    [SerializeField] private int _levelToLoad = 0;
    private bool _recordLevel = false;

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

    [SerializeField] private Camera _camera;

    private GameObject _player;

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
        //shPos.position = new Vector2(1, (arr.OrderByDescending(x => x.Count).First().Count + 2)/2);
        //shPos.localScale = new Vector3( arr.Count + 2,arr.OrderByDescending(x => x.Count).First().Count + 2, arr.OrderByDescending(x => x.Count).First().Count + 2);
        shPos.position = new Vector2(0.5f,0.5f);
        shPos.localScale = new Vector3((arr.Count + 2) * 5, arr.OrderByDescending(x => x.Count).First().Count + 2, arr.OrderByDescending(x => x.Count).First().Count + 2);

        _camera.transform.position = _player.transform.position;
        _camera.transform.position += new Vector3(0, 0, -1);
        _camera.transform.SetParent(_player.transform);
        //_camera.orthographicSize = arr.Count/2 + 1;
    }
}
