using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPortal : Cell, ISpawner
{
    private Dictionary<Monster,int> _toSpawn = new Dictionary<Monster, int>();
    public void Spawn()
    {
        
    }

    public void AddToSpawner(Monster monster, int count)
    {
        if (_toSpawn.Where(x => x.Key == monster) != null)
            _toSpawn[monster] += count;
        else
            _toSpawn.Add(monster, count);
    }

    public void ChangeSpawner()
    {

    }

    public PortalInfo getPortalInfo(int[,] map)//заповнення MapInfo для передачі
    {
        PortalInfo info = new PortalInfo();
        info.position = new Vector2Int((int)transform.position.x,(int)transform.position.y);
        info.FindWays(map);
        Debug.Log(info.ways.Count);
        entityInfo(info.monsters);
        return info;
    }

    private void entityInfo(List<EntityToSpawn> entities)
    {
        entities.Clear();
        foreach (var entity in _toSpawn)
            entities.Add(new EntityToSpawn(entity.Key.name.Remove(entity.Key.name.IndexOf("(Clone)")), entity.Value));
    }
}

public interface ISpawner
{
    void Spawn();
}

[System.Serializable]
public class Way//Шлях і методи його подання для збереження в JSON і подальшого використання
{
    public List<Vector2Int> way = new List<Vector2Int>();
    public bool[,] visitedMap;

    public Way(Vector2Int point, int mapX, int mapY)
    {
        way.Add(point);
        createEmptyVisitedMatrix(mapX, mapY);
        visitedMap[point.x, point.y] = true;
    }

    public Way(Way way, Vector2Int point)
    {
        visitedMap = new bool[way.visitedMap.GetLength(0), way.visitedMap.GetLength(1)];
        for (int i = 0; i < way.visitedMap.GetLength(0); i++)
            for (int j = 0; j < way.visitedMap.GetLength(1); j++)
                visitedMap[i, j] = way.visitedMap[i, j];



        this.way.AddRange(way.way);
        this.way.Add(point);
        visitedMap[point.x, point.y] = true;
    }

    private void createEmptyVisitedMatrix(int x, int y)
    {
        visitedMap = new bool[x, y];
    }

    public static bool operator !=(Way way1, Way way2)
    {
        if (way1.way.Count != way2.way.Count)
            return true;
        for (int i = 0; i < way1.way.Count; i++)
            if (way1.way[i] != way2.way[i])
                return true;

        return false;
    }

    public static bool operator ==(Way way1, Way way2)
    {
        if (way1.way.Count != way2.way.Count)
            return false;
        for (int i = 0; i < way1.way.Count; i++)
            if (way1.way[i] != way2.way[i])
                return false;

        return true;
    }
}

[System.Serializable]
public class EntityToSpawn//інформація про кожного монстра, який появиться зі спавнера для збереження в JSON
{
    public string path = "Assets/Resources/Monsters/";
    public int count = 1;
    public EntityToSpawn(string path, int count)
    {
        this.path += path;
        this.count = count;
    }
}