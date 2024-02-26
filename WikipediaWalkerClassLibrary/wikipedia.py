import wikipediaapi

def get_api_page(page_title):
     # Создаем объект Wikipedia
    wiki_wiki = wikipediaapi.Wikipedia(language='en', user_agent="ShortestPathFinder/1.0")

    # Получаем страницу из API
    page = wiki_wiki.page(page_title)
    return page

# Проверяем, существует ли страница
def is_page_exists(page_title):

    page = get_api_page(page_title)
    return page.exists()

def get_page_links(page_title):

    # Проверяем, существует ли страница
    if not is_page_exists(page_title):
        return []
    
    page = get_api_page(page_title)

    # Получаем ссылки на другие страницы
    links = page.links

    # Возвращаем список названий ссылок
    return sorted(links.keys())
