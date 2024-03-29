# AssignmentReporter

AssignmentReporter - это инструмент для генерации шаблонных отчётов о выполнении университетских работ. Основная идея проекта - научиться генерировать шаблонный отчет на основании репозитория с кодом. В большинстве случаем, именно такой отчёт и требуют. Как этим должны пользоваться:

1. Студент выполняет задание, загружает решение (код) в свой репозиторий на Github
2. Студент запускает AssignmentReporter и указывает этот репозиторий
3. Система клонирует репозиторий, находит все файлы с кодом в репозитории
4. Система создаёт файл отчёта, вставляет листанг кода и сохраняет результат
5. Студент получает отчёт и лишние 30 минут времени, которые он экономит

## Гайд по запуску

1. В функции main создайте экземпляр нужного генератора отчётов, на данный момент поддерживаются:
   1. MarkdownReportGenerator (собирает код содержимое из данных файлов в 
      один с форматированием для определённого языка)
2. В функции main создайте экземпляр конфигурации генерации отчёта, указав путь до
   директории со входными данными, путь до директории сохранения, имя файла с отчётом, маски 
   файлов для игнорирования и исключений из игнорирования (как в gitignore)
3. Запустите программу и наслаждайтесь результатом
   
## Функционал

- выгрузка из локальной файловой системы и из гитхаба
- фильтрация файлов по расширению (whitelist и blacklist)
- добавление дополнительной информации - комментарии для вступления и вывод
- выбор файлов только по определенной подпапке (для случаев, когда в репозитории несколько папок)
- поддержка генерации в .docx, pdf, markdown
- множественная генерация отчетов с делением на подпапки
- добавление шаблонного титульного листа
- поддержка приватных репо
- Массовая генерация отчётов для всех репозиториев в организации
