devel: ![GitHub Logo](https://travis-ci.com/eaniconer/LinguaDay-web.svg?token=EwyvQqYCEe8LLA1J7gu5&branch=devel)

# MediaStudio
Web Application for christian media content with using  .NET CORE  technology


##### How to contribute

###### Что нужно сделать перед тем как приступить к новой задаче

0. Если репозиторий еще не склонирован
`git clone https://github.com/ArtemNemtsov/MediaStudio`

1. Переключаемся на ветку devel и подтягиваем свежую версию из удаленного репозитория

```
git checkout devel
git pull
```

2. Все задачи оформляются в виде [Issues](https://github.com/ArtemNemtsov/MediaStudio/issues). У каждого issue есть номер N. 
Для начала нужно убедиться, что текущая ветка имеет имя `devel`. Далее выполнить следующую команду:

`git checkout -b login/task-N`

Вместо `login` нужно использовать свой логин на github.
Вместо `N` нужно подставить номер issue.

3. Далее пишем код / тесты / фиксим баги. Коммитим изменения и отправляем в удаленный репозиторий.

```
git add .
git commit -m "Comments"
git push -u origin HEAD
```

4. Если таска выполнена, то нужно создать Pull Request в ветку devel

5. Проходим ревью, исправляем замечания (команды аналогичны пункту 3). Если все ок, то ветка с задачей будет слита с веткой `devel`. После этого можно выполнить следующие команды:

```
git checkout devel
git branch -D login/task-N
git pull
```
Удаляем ветку в которой работали, чтобы не мешала в дальнейшей работе.
И подтягиваем изменения из удаленного репозитория.





