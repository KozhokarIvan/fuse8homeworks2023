<details open>
    <summary><h1>Описание</h1></summary>
    
    
Проект является API для работы с курсами валют и представляет из себя два сервиса: `PublicAPI` и `InternalAPI` которые общаются между собой через gRPC. `InternalAPI` обращается к [стороннему API](https://currencyapi.com/) и кеширует все запросы в базу данных

### Функционал:

- Получение курсов валют
- Сохранение именованной пары валют в избранное для будущего получения курсами
- Изменение настроек
- Изменение базовой валюты и пересчет кеша относительно новой базовой валюты с помощью фоновых задач

### Реализация основного проекта состояла из нескольких этапов:

- Реализация работы с внешним API, логирования запросов, настройка фильтров и конфигурации приложения ([посмотреть задание](Homeworks/Homework3/ReadMe.md))
- Разделение проекта на `InternalAPI` и `PublicAPI`, настройка кэширования запросов в файловую систему, настройка взаимодействия проектов по gRPC ([посмотреть задание](Homeworks/Homework4/ReadMe.md))
- Изменение хранилища кэша с файла на базу данных PostgreSQL, доработка API, добавление функционала для именованных избранных пар валют ([посмотреть задание](Homeworks/Homework5/ReadMe.md))
- Упаковка приложения в docker, реализация пересчета кэша относительно новой базовой валюты ([посмотреть задание](Homeworks/Homework6/ReadMe.md))

</details>

### [_Инструкция по запуску_](example/README.md)

<details open>
    <summary><h1>Swagger</h1></summary>
    <details open>
        <summary><img src="assets/endpoints-interactive/public/title.png" /></summary>
        <details>
            <summary><img src="assets/endpoints-interactive/public/currency/title.png" /></summary>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/currency/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/currency/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/currency/currency/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/currency/currency/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/currency/currency/date/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/currency/currency/date/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive//public/currency/favorites/name/date/get.png" /></summary>
                    <img src="assets/endpoints-interactive//public/currency/favorites/name/date/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/currency/currency/date/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/currency/currency/date/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/settings/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/settings/get-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/settings/currency/post.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/settings/currency/post-expanded.png" />
                </details>
                <details>
                    <summary><img src="assets/endpoints-interactive/public/currency/settings/decimal-places/post.png" /></summary>
                    <img src="assets/endpoints-interactive/public/currency/settings/decimal-places/post-expanded.png" />
                </details>
        </details>
        <details>
            <summary><img src="assets/endpoints-interactive/public/exchanges/title.png" /></summary>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/exchanges/favorites/name/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/name/get-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/exchanges/favorites/name/put.png" /></summary>
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/name/put-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/exchanges/favorites/name/delete.png" /></summary>
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/name/delete-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/exchanges/favorites/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/get-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/exchanges/favorites/post.png" /></summary>
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/post-expanded1.png" />
                    <img src="assets/endpoints-interactive/public/exchanges/favorites/post-expanded2.png" />
            </details>
        </details>
        <details>
            <summary><img src="assets/endpoints-interactive/public/healthcheck/title.png" /></summary>
            <details>
                    <summary><img src="assets/endpoints-interactive/public/healthcheck/get.png" /></summary>
                    <img src="assets/endpoints-interactive/public/healthcheck/get-expanded.png" />
            </details>
        </details>
    </details>
    <details open>
        <summary><img src="assets/endpoints-interactive/internal/title.png" /></summary>
        <details>
            <summary><img src="assets/endpoints-interactive/internal/currency/title.png" /></summary>
            <details>
                    <summary><img src="assets/endpoints-interactive/internal/currency/get.png" /></summary>
                    <img src="assets/endpoints-interactive/internal/currency/get-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/internal/currency/date/get.png" /></summary>
                    <img src="assets/endpoints-interactive/internal/currency/date/get-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/internal/currency/settings/get.png" /></summary>
                    <img src="assets/endpoints-interactive/internal/currency/settings/get-expanded.png" />
            </details>
            <details>
                    <summary><img src="assets/endpoints-interactive/internal/currency/cache/post.png" /></summary>
                    <img src="assets/endpoints-interactive/internal/currency/cache/post-expanded.png" />
            </details>
        </details>        
        <details>
            <summary><img src="assets/endpoints-interactive/internal/healthcheck//title.png" /></summary>
            <details>
                    <summary><img src="assets/endpoints-interactive/internal/healthcheck/get.png" /></summary>
                    <img src="assets/endpoints-interactive/internal/healthcheck/get-expanded.png" />
            </details>
        </details>
    </details>

</details>

<details open>
    <summary><h1>Демонстрация работы</h1></summary>
    <details>
        <summary><h4>Получение настроек</h4></summary>
        <img src="assets/demo/gifs/GetSettings.gif" />
        <img src="assets/demo/gifs/GetSettings.png" />
    </details>
    <details>
        <summary><h4>Получение текущего курса валюты по умолчанию относительно курса базовой валюты</h4></summary>
        <img src="assets/demo/gifs/GetCurrentCurrency.gif" />
        <img src="assets/demo/gifs/GetCurrentCurrency.png" />
    </details>
    <details>
        <summary><h4>Создание фоновой задачи на смену базовой валюты</h4></summary>
        <img src="assets/demo/gifs/ChangeBaseCurrencyTask.gif" />
        <img src="assets/demo/gifs/ChangeBaseCurrencyTask1.png" />
        <img src="assets/demo/gifs/ChangeBaseCurrencyTask2.png" />
        <img src="assets/demo/ChangeBaseCurrencyTaskStatuses.png" />
        <img src="assets/demo/ChangeBaseCurrencyTaskCreated.png" />
        <img src="assets/demo/ChangeBaseCurrencyTaskSucceded.png" />
    </details>
    <details>
        <summary><h4>Получение текущего курса валюты по умолчанию относительно курса новой базовой валюты</h4></summary>
        <img src="assets/demo/gifs/GetCurrencyAfterBaseChanged.gif" />
        <img src="assets/demo/gifs/GetCurrencyAfterBaseChanged.png" />
    </details>
    <details>
        <summary><h4>Создание избранной пары валют для быстрого получения курса</h4></summary>
        <img src="assets/demo/gifs/AddedFavoriteCurrency.gif" />
        <img src="assets/demo/gifs/AddedFavoriteCurrency1.png" />
        <img src="assets/demo/gifs/AddedFavoriteCurrency2.png" />
    </details>
    <details>
        <summary><h4>Вывод всех избранных пар валют</h4></summary>
        <img src="assets/demo/gifs/DisplayedAllFavoriteCurrencies.gif" />
        <img src="assets/demo/gifs/DisplayedAllFavoriteCurrencies.png" />
    </details>
    <details>
        <summary><h4>Вывод параметров избранной пары валют по ее названию</h4></summary>
        <img src="assets/demo/gifs/DisplayedFavoriteCurrencyByName.gif" />
        <img src="assets/demo/gifs/DisplayedFavoriteCurrencyByName1.png" />
        <img src="assets/demo/gifs/DisplayedFavoriteCurrencyByName2.png" />
    </details>
    <details>
        <summary><h4>Вывод курса для избранной пары валют</h4></summary>
        <img src="assets/demo/gifs/DisplayedFavoriteExchangeRateByName.gif" />
        <img src="assets/demo/gifs/DisplayedFavoriteExchangeRateByName1.png" />
        <img src="assets/demo/gifs/DisplayedFavoriteExchangeRateByName2.png" />
    </details>
    <details>
        <summary><h4>Удаление избранной пары валют</h4></summary>
        <img src="assets/demo/gifs/DeletedFavoriteCurrencyByName.gif" />
        <img src="assets/demo/gifs/DeletedFavoriteCurrencyByName1.png" />
        <img src="assets/demo/gifs/DeletedFavoriteCurrencyByName2.png" />
    </details>
</details>

### Фидбек от ведущего разработчика fuse8 на момент сдачи проекта:

> Понравилось:
> логичное разбиение проектов по папкам, все сервисы, сущности, репозитории находятся именно там где и ожидаешь
> Реализовал правильные репозитории
> Говорил по делу

> Не понравилось:
> В некоторых местах очень не хватало комментариев к тому, что происходит в методах.
> Так же для лучшего понимания кода хорошо бы выносить части кода в приватные методы, не делать всю логику в одном методе
> Небольшой косяк в разделении логики - Избранные по идее должны быть только в PublicApi, InternalApi это просто обертка над внешней апишкой, об Избранных ему знать незачем, хорошо бы переименовать все что связано с Favorite в InternalApi во что то более абстрактное
