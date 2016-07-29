# Publician
## 0. Getting started
This project **Publician** was made with Unity 5.3.3 using C# language for scripting. After cloning this repository you will need
to open the contents as a project in Unity. 

This is a demo application that was developed for a very short time. All feedback is welcome.

## 1. Project description

**Publician** is an application that provides searching possibilities on the [Yle api](http://developer.yle.fi/index.en.html).
You can make a free-text search on the API by supplying a string query. The Finnish title of each result is displayed in a scrollable list.
Only 10 results per page are loaded, and when the end of the list is reached, the next 10 results are fetched and displayed.


While the fetching is in progress an spinning bar is shown at the end of the list with a fetching message. 
If the fetching fails for some reason (for example: no internet connection) an error message is displayed. 

<img src="/Screenshots/Screenshot01.png" width="250">
<img src="/Screenshots/Screenshot02.png" width="250">
<img src="/Screenshots/Screenshot03.png" width="250">
<img src="/Screenshots/Screenshot04.png" width="250">

## 2. Architecture

The main components of the architecture are:
- **SearchAPIComponent** - the main controller that is responsible for the search feature. It handles the interactions and fetching of the content.

- **SearchItemsListViewComponent** - responsible for showing a list of items in a contaiter. It receives logic data and instantiates and shows a view objects of the data. 
It uses an object pool for the instantiated views.

- **DynamicContentScrollRect** - a custom ScroolRect with the option an ContentUpdater to update its contents when the scrolling has reached the end of the list.

## 3. Notable features

## 3.1. API requests/responses

The api requests were done using the **UnityWebRequest** system for sending requests and fetching responses. 
The response objects were converted to JSON objects using the standard **JSON Serializer**. A plain C# classes were created, representing
the structured JSON object needed for the serialization.

## 3.2. Object pool

Object pool were introduced for the list items objects that are dynamically instantiated multiple times while scrolling the list.

## 3.3. Constants
All the global constants that are needed in the code of the project is stored in the Constants file.

## 3.4. UI

The UI was done using the standard Unity UI with some additional added components. 
All of the screens were designed to be mobile-friendly and to be viewable on wide-range of devices.
Layouting was used to achieve best results for different devices.

The following additional UI components were added that can be reused:
- **DynamicContentScrollRect** - a custom ScroolRect with the option an ContentUpdater to update its contents when the scrolling has reached the end of the list.


