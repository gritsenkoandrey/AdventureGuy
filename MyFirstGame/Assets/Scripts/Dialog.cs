﻿using UnityEngine;


// Сериализация представляет процесс преобразования какого-либо объекта в поток байтов.
//После преобразования мы можем этот поток байтов или записать на диск или сохранить его временно в памяти.
[System.Serializable]
public class Dialog
{
    #region Fields

    [SerializeField] internal string _name;

    [TextArea(4, 10)] // увеличение окошек в Unity для ввода текста в массив (string[] _sentences).
    [SerializeField] internal string[] _sentences;

    #endregion
}

//Привет друг! У нас с тобой сложное задание, нужно провести меня к той груде камней, возможно там спрятано что-то ценное.
//А теперь пожалуйста ознакомься с кратким руководством! Это очень важно для меня.
//Правило первое: избегай колючих предметов. От соприкосновения с ними у меня полыхает в пятой точке.
//Правило второе: монстры которые передвигаются и стреляют можно убить только прыжком на них сверху.
//Правило третье: монстров, которые стоят на месте, можно убить стреляя по ним.
//Ну и на последок! У тебя пять жизней, но их можно восполнить собирая сердца, а также можно временно усилить прыжок! Удачи!