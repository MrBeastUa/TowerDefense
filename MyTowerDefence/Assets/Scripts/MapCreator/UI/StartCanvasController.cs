using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvasController : MonoBehaviour
{
    //відповідає за початкові настройки: ім'я мапи, розміри та пакети графіки 
    [SerializeField]
    private InputField _x, _y, _name, _seed;
    [SerializeField]
    private Dropdown _enviromental;
    [SerializeField]
    private GameObject _difficultyPanel;

    private Package enviromental;
    private AccItemsInfoLoad _infoLoader;

    private void OnEnable()
    {
        _infoLoader = AccItemsInfoLoad.Instance;

        if (_enviromental.options.Count == 0 && _infoLoader.EnviromentalPackages.Count != 0)
            _enviromental.options.AddRange(_infoLoader.EnviromentalPackages.Select(x => new Dropdown.OptionData() { text = x.Name }));

        enviromental = _infoLoader.EnviromentalPackages[0];
    }

    public void OpenDifficultyPanel()
    {
        _difficultyPanel.SetActive(!_difficultyPanel.activeSelf);
    }

    public void SetStartData()
    {
        int x, y, seed;

        if(File.Exists($"Assets/Resources/Maps/{_name.text}.json"))
        {
            ErrorOutput.instance.OutputError("Such a map name already exists");
        }
        else if (_name.text == "")
        {
            ErrorOutput.instance.OutputError("Name field can`t be empty");
        }
        else if (!int.TryParse(_x.text, out x))
        {

        }
        else if (!int.TryParse(_y.text, out y))
        {

        }
        else if(!int.TryParse(_seed.text, out seed))
        {

        }
        else
            MapCreator.Instance.StartEditMode(_name.text, x, y, seed, enviromental);
    }
}
