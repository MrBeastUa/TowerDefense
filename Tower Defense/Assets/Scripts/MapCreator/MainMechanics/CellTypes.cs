using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangableCell
{
    void ChangeCell();
}

public interface IStartPoint
{
    void SetMonstersList();
}

public interface IEndPoint
{
    void SetAgressiveMonstersToThisPoint();
}

public interface IBuildPoint
{
    void BuildAttackTower();
}