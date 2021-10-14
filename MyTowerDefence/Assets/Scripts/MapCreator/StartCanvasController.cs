using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvasController : MonoBehaviour
{
    //відповідає за початкові настройки: ім'я мапи, розміри та пакети графіки 
    [SerializeField]
    private InputField _x, _y, _name;
    [SerializeField]
    private Dropdown _decor, _enviromental;
    private Package decor, enviromental;
    private AccItemsInfoLoad _infoLoader;
    private void OnEnable()
    {
        _infoLoader = AccItemsInfoLoad.Instance;

        if (_decor.options.Count == 0 && _infoLoader.DecorPackages.Count != 0)
            _decor.options.AddRange(_infoLoader.DecorPackages.Select(x => new Dropdown.OptionData() { text = x.Name }));
        if (_enviromental.options.Count == 0 && _infoLoader.EnviromentalPackages.Count != 0)
            _enviromental.options.AddRange(_infoLoader.EnviromentalPackages.Select(x => new Dropdown.OptionData() { text = x.Name }));

        if(decor != null)
        decor = _infoLoader.DecorPackages[0];
        enviromental = _infoLoader.EnviromentalPackages[0];
    }

    public void SetStartData()
    {
        int x, y;

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
        else
            MapCreator.Instance.StartEditMode(_name.text, x, y, enviromental, decor);
    }
}
