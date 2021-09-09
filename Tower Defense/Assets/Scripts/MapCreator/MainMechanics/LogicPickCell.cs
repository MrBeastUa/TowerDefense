using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LogicPickCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _storage, _packageExample;

    private Dictionary<GameObject, List<GameObject>> _packagesInStorage = new Dictionary<GameObject, List<GameObject>>();
    private List<Package> _allPackages = new List<Package>();

    private void Start()
    {
        SetPackages(DataLoader.instance.Packages);
    }

    private void SetPackages(List<Package> packages)
    {

        if (_allPackages != null && packages.Count > 0)
        {
            _allPackages.AddRange(packages);
            _allPackages.ForEach(x => AddPackageToStore(x, this));
        }
    }
    private void AddPackageToStore(Package package, LogicPickCell logic)
    {
        GameObject newPackage = Instantiate(_packageExample, _storage.transform);
        newPackage.GetComponent<PackageScript>().CreatePackage(package, logic);
        _packagesInStorage.Add(newPackage, Resources.LoadAll<GameObject>(package.Path).ToList());
    }

    public void PickCell(GameObject package, int itemId)
    {
        foreach (var pack in _packagesInStorage)
            if (pack.Key == package)
                if (itemId >= 0)
                    MapCreator.instance.currentCellToPlace = pack.Value[itemId];
                else MapCreator.instance.currentCellToPlace = null;
            else
                pack.Key.GetComponent<PackageScript>().SelectNullCell();
    }
}