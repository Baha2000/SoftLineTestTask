# SoftLineTestTask

## Стек

* ASP.NET Core 3.1
* SQL Server

## Setup

* Убедиться что у вас установлено ядро MS SQL.
* Выполнить Update-Database для создания базы данных.
* Убедиться что создалась база данных(например через SQL Management Studio).
* Запустить не через IIS, а выбрать из выпадающего списка SoftLineTestTask.

## Описание задания

Разработать приложение по следующим требованиям:
- Разработать базу данных с двумя таблицами
    1. Задачи, содержит столбцы (ID, Name, Description, Status_ID).
    2. Статусы, содержит столбцы (Status_ID, Status_name). Необходимо создать 3 строки со значениями (Создана, В работе, Завершена). Идентификаторы придумать самостоятельно.

- Разработать интерфейс отображающий:
    1. Вкладка Задачи, содержит саму таблицу "Задачи", кнопки "Добавить",
"Удалить", "Редактировать". Должна быть возможность выделять строку для
удаления или редактирования.
Таблица "Задачи" должна визуально содержать следующие столбцы
(Наименование(Name), Описание(Description), Статус (Значение поля Status_name
из таблицы "Статусы", связь осуществляется по полю Status_ID)
    2. Кнопка "Добавить" - По нажатию на кнопку открывается форма добавления новой
записи, заполняются данные и нажимается кнопка "Добавить", должна быть
реализована проверка на заполненность всех полей.
    3. Кнопка "Удалить" - По нажатии на кнопку происходит удаление выделенной
записи таблицы "Задачи", если не выбрана запись пользователь должен получить
соответствующее сообщение.
    4. Кнопка "Редактировать" - По нажатию на кнопку открывается форма
редактирования выделенной записи.
На формах редактирования и добавления записи поле статус должно
представлятьвыпадающий список со значениями поля (Status_name) из таблицы
Статусы.

- Операции по получению, удалению и обновлению записей должны использовать
API контроллеры и выполнены в виде вызова методов Web API. Для передачи
данных необходимо использовать JSON.