﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance => _instance;

    public GameState State { get; private set; } = GameState.Menu;
    public Dataset CurrentDataset = new Dataset()
    {
        name = "ООП C#", stars = 0,
        Questions = new List<Question>()
        {
            new Question("Поліморфізм - це", "можливість використовувати об'єкта похідного класу, як батьківського, включаючи всі доступні поля."),
            new Question("Наслідування - це", "здатність класів наслідувати поведінку та поля іншого класу"),
            new Question("Інкапсуляція - це", "механізм об'єднання, безпечного зберігання та використання даних і методів обробки цих даних"),
            new Question("Абстрактний клас - це", "клас, який створюється лише в цілях наслідування і може містити методи з не об'явленою реалізацією (абстрактні)"),
            new Question("Інтерфейс - це", "тип даних, який дозволяє оголосити властивості, методи, івенти без їх реалізації." +
                                            " Наслідування дозволяє реалізовувати кілька інтерфейсів"),
            new Question("Ключове слово override відповідає за","перевизначення абстрактних чи віртуальних методів, властивостей, івентів і т.д. задаючи їм функціонал" +
                                                                 "який буде доступний лише класу, в якому відбулось перевантаження і в похідних від нього"),
            new Question("Перевантаження - це","один з підтипів поліморфізму(ad hoc поліморфізм), який дозвольяє оголошувати методи з однаковою назвою, " +
                                                "але з різними наборами даних, які він може приймати"),
            new Question("Клас Object - це","головний клас, від якого наслідуються всі структури і класи(включно з базовими типами даних). Всі створені користувачем " +
                                            "класи і структури по замовчуванню є похідними від нього."),
            new Question("Приведення типів - це","можливість розглядати об'єкт похідного класу, як об'єкт батьківського, а також можливість в подальшому " +
                                                "повернутись до розгляду даного об'єкта, як до об'єкта похідного класу."),
            new Question("Віртуальні методи - це","методи, які містять в собі часткову чи повну реалізацію, але при потребі, можуть бути перевизначені в похідних класах"),
            new Question("Приховування методів(hiding) - це","оголошення полів в похідному класі з допомогою ключового слова new, що дозволяє приховати" +
                                                     "функціонал, описаний в батьківському класі."),
            new Question("Модифікатор private - це","модифікатор доступу, що приховує поля від будь-якого виклику за межами класу. Також може використовуватись " +
                                                    "для класів чи структур і в такому разі забороняє використовувати їх в інших просторах імен."),
            new Question("Модифікатор protected - це","модифікатор доступу, що приховує поля від будь-якого виклику, окрім викликів в базовому класі та похідних від нього"),
            new Question("Модифікатор public - це","модифікатор доступу, що дозволяє використовувати поля в будь якийх класах. " +
                                                    "Також може використовуватись для класів чи структур і надає їм доступ з будь-якого простору імен"),
            new Question("Модифікатор internal - це","модифікатор доступу, що дозволяє використовувати поля в межах даного проекту. Так само даний модифікатор" +
                                                     "працює і з класами. Приклад : при підключенні даного проекту, як бібліотеки для іншого проекту все," +
                                                     " що позначене, як internal фідображатись не буде!"),
            new Question("Модифікатор protected internal","модифікатор доступу, що дозволяє використовувати поля в межах даного проекту чи в похідних від класу, в якому вони оголошені"),
            new Question("",""),
        }
    };
    public GameProcessData CurrentMapData;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public void changeState(GameState state)
    {
        if (State == state)
            return;

        State = state;
    }

}

public enum GameState
{
    Menu,
    Game,
    MapCreator
}
