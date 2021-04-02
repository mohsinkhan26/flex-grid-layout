# Overview Flexible Grid Layout #

### Introduction ###
As couldn't find any reliable Unity UI solution for the flexible grid layout, so here it is

### Purpose ###
Flexible grid layout used majorly for tags, and to show selected options

### Unity Asset Store Link ###

[http://u3d.as/2sQ8](http://u3d.as/2sQ8)

### Features ###
* Easily integrate Flexible Grid Layout in your game
* Vertical and horizontal
* Easy to customize using asset file
* Easy to use multiple styles with different customizations using asset file
* Easy to use, Plug n play
* 12 example scenes
* Made with Unity UI
* Fully customizable
* Open Source code without any DLL

### Usage ###
* Import plugin
* Drag FlexGridLayout (horizontal or vertical) prefab under your UI canvas
* Add/Drag your customized FlexGrid Data asset file in FlexGridLayout.cs from the inspector
![Flex Grid Layout.jpg](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Screenshots/Flex%20Grid%20Layout.jpg)
* Now, you are good to go

### Customizations ###
FlexGrid Data asset file can be customized according to your need
![Flex Grid Data.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Screenshots/Flex%20Grid%20Data.png)
1. TRUE: for Vertical layout (items will be horizontal), FALSE: for Horizontal layout (items will be vertical)
2. Items will show cross button or not, to delete
3. Items will show add button or not, to add a new item
4. Items are interactable as toggle or not
5. **It defines the maximum limit of text in one row(Vertical layout)/column(Horizontal layout). You have to adjust this value very carefully according to your need, _otherwise, items will overlap_**
6. Flexible Grid prefab reference
7. Flexible Grid Item prefab reference, you can customize the style according to your UI style
8. How you want to align your items in the flexible grid
9. Whether you want to share Add button as the last item of the grid, or not
10. Space required to show the add button as the last item. As you can show the image with/without text, but still it needs the space as the last item
11. Flexible Grid Last Item prefab reference, you can customize the style according to your UI style
12. Last item of Add button image
13. Whether you want to show the text in the last item or not
14. Last add item text

### Examples ###
Vertical Example
![Vertical Example-00.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-00.png)

Vertical Example - Text before buttons
![Vertical Example-01-Text before buttons.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-01-Text%20before%20buttons.png)

Vertical Example - Without Add button in items
![Vertical Example-02-Without Add in items.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-02-Without%20Add%20in%20items.png)

Vertical Example - Without Add and Cross buttons in items
![Vertical Example-03-Without Add and Cross in items.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-03-Without%20Add%20and%20Cross%20in%20items.png)

Vertical Example - With NO Add and Cross buttons
![Vertical Example-04-With NO Add and Cross.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-04-With%20NO%20Add%20and%20Cross.png)

Vertical Example - With NO Add and Cross buttons + Non-Interactable
![Vertical Example-05-With NO Add and Cross + Non-Interactable.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Vertical%20Example-05-With%20NO%20Add%20and%20Cross%20+%20Non-Interactable.png)

Horizontal Example
![Horizontal Example-00.png](https://github.com/mohsinkhan26/flex-grid-layout/blob/main/Examples/Horizontal%20Example-00.png)

### Remember ###
* If you put the plugin under any other folder (except Assets), then replace the path accordingly in `AssetDataHelper.cs`

### Special Thanks ###

* All the users who provide feedback and suggestions to improve
* All the users who gave reviews on the Asset store


## Thanks for your support! ##