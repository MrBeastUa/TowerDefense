using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PortalInfo
{
    public Vector2Int position;
    public List<EntityToSpawn> monsters = new List<EntityToSpawn>();
    public List<Way> ways = new List<Way>();

    public void FindWays(int[,] map)
    {
        ways.Add(new Way(position, map.GetLength(0), map.GetLength(1)));
        int nextCountWays = 1;
        while (nextCountWays != 0)//створення всіх можливих варіацій шляхів і додання в List
        {
            Debug.Log(ways.Count);
            List<Way> newWays = new List<Way>();
            nextCountWays = 0;
            foreach (Way way in ways)//для кожного шляху знаходимо сусідні клітинки для останнього елемента
            {
                List<Vector2Int> nextPoints = getNeighborhood(way, map);

                //якщо точок нових нема зберігає поточний шлях в іншому випадку створює новий шлях для кожної сусідньої точки
                if (nextPoints.Count > 0)
                    foreach (Vector2Int point in nextPoints)
                    {
                        newWays.Add(new Way(way, point));
                        nextCountWays++;
                    }

                else
                    newWays.Add(way);
            }

            if (newWays.Count > 0)
            {
                ways.Clear();
                ways.AddRange(newWays);
            }
        }

        //видалення тупикових шляхів та шляхів, які повторяються
        List<Way> toDel = ways.Where(way => map[way.way.Last().y, way.way.Last().x] != 3).ToList();

        for (int i = 0; i < ways.Count; i++)
        {
            for (int j = i; j < ways.Count; j++)
            {
                if (ways[i] == ways[j] && i != j)
                    toDel.Add(ways[i]);
            }
        }

        foreach (Way way in toDel)
            ways.Remove(way);
    }

    private List<Vector2Int> getNeighborhood(Way way, int[,] points)//пошук сусідніх клітинок на які можна перейти
    {

        Vector2Int point = way.way.Last();
        List<Vector2Int> result = new List<Vector2Int>();
        if (points[point.x, point.y] > 0 && points[point.x, point.y] < 3)
        {
            if (point.y == 0)
            {
                if (!way.visitedMap[point.x, point.y + 1] && (points[point.x, point.y + 1] == 1 || points[point.x, point.y + 1] == 3))
                    result.Add(new Vector2Int(point.x, point.y + 1));
            }
            else if (point.y == points.GetLength(1) - 1)
            {
                if (!way.visitedMap[point.x, point.y - 1] && (points[point.x, point.y - 1] == 1 || points[point.x, point.y - 1] == 3))
                    result.Add(new Vector2Int(point.x, point.y - 1));
            }
            else
            {
                if (!way.visitedMap[point.x, point.y + 1] && (points[point.x, point.y + 1] == 1 || points[point.x, point.y + 1] == 3))
                    result.Add(new Vector2Int(point.x, point.y + 1));
                if (!way.visitedMap[point.x, point.y - 1] && (points[point.x, point.y - 1] == 1 || points[point.x, point.y - 1] == 3))
                    result.Add(new Vector2Int(point.x, point.y - 1));
            }

            if (point.x == 0)
            {
                if (!way.visitedMap[point.x + 1, point.y] && (points[point.x + 1, point.y] == 1 || points[point.x + 1, point.y] == 3))
                    result.Add(new Vector2Int(point.x + 1, point.y));
            }
            else if (point.x == points.GetLength(0) - 1)
            {
                if (!way.visitedMap[point.x - 1, point.y] && (points[point.x - 1, point.y] == 1 || points[point.x - 1, point.y] == 3))
                    result.Add(new Vector2Int(point.x - 1, point.y));
            }
            else
            {
                if (!way.visitedMap[point.x + 1, point.y] && (points[point.x + 1, point.y] == 1 || points[point.x + 1, point.y] == 3))
                    result.Add(new Vector2Int(point.x + 1, point.y));
                if (!way.visitedMap[point.x - 1, point.y] && (points[point.x - 1, point.y] == 1 || points[point.x - 1, point.y] == 3))
                    result.Add(new Vector2Int(point.x - 1, point.y));
            }
        }
        return result;
    }
}
