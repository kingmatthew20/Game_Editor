   M S B A M L   `   `   `  ����   Game Editor    Game_Editor.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Game Editor��$	��512��$	��768��.��� .+�o�  o� 
IsMainMenu . 
�������$��25��$	��Top=�.@�m� $
��File��.@�m� $	��New��m� $
��Save�� m� Command$ ApplicationCommands.Save��m� $
��Load��$ ApplicationCommands.Open��m� -   $
��Exit��m� $
��Edit��.@�m� $
��Undo��m� $
��Redo��m� $	��Map��.@�m� $��
Load Tiles��$! ApplicationCommands.Replace��m� $��Save Map���� -   $��tabCtrl��$��0��$��
0,30,9.6,1q�$��2��.@��� $��	Character��.��� -   $��	gridLevel�� � ColumnDefinitions �� $	��275 ��� �� $	��200 � � RowDefinitions �� $��18 ��� �� $	��189 �.+��� 
���   �$��1q�$��1��$��2,10,0,0q�$��1��$	��192��$	��Top=�._��� -   $��knight��$��1��$��8,8,9,9q�$��1���� -   $��Colour��$��2��$��
10,10,10,0q�$��1��$��Disabled=�$��Visible=�$	��192��$	��Top=�.@��� -   $��Green��.����  �� Orientation$ 
Horizontal=�.+��� $K��EU:\2nd Year\MIG\ICA\Game Editor\Game Editor\resource\bullet_green.png��$��0,0,6,0q�$��16���� Green�� -   $
��Blue��.���� $ 
Horizontal=�.+��� $J��DU:\2nd Year\MIG\ICA\Game Editor\Game Editor\resource\bullet_blue.png��$��0,0,6,0q�$��16���� Blue�� -   $	��Red��.���� $ 
Horizontal=�.+��� $I��CU:\2nd Year\MIG\ICA\Game Editor\Game Editor\resource\bullet_red.png��$��0,0,6,0q�$��16���� Red�� -	   $
��Grey��.���� $ 
Horizontal=�.+��� $J��DU:\2nd Year\MIG\ICA\Game Editor\Game Editor\resource\bullet_grey.png��$��0,0,6,0q�$��16���� Grey�� -
   $��Load_colour��$��Load Colour��$��2��$
��Left=�$��19��$��10,1,0,0q�$��2��$	��Top=�$	��180���� $��Character Sprite��$��1��$
��Left=�$��29��$��	10,-1,0,0q�$	��Top=�$	��118��$��2���� $��Colour��$��2��$
��Left=�$��10,0,0,0q�$	��Top=�$	��111��$��23��$��2���� $
��Left=�$��25��$��	94,10,0,0q� �� TextWrapping$
 Wrap=�$��0��$	��Top=�$��40��$��1���� $��0,10,181,167q�$��Character Lives:��$��0��$��1���� $��94,50,33,131q�$��1�� �� SelectedIndex$ 3��.@��� $��0.25X��$
��Left=�$	��146��$��2,0,-2,0q��� $
��0.5X���� $��0.75X��$
��Left=�$	��146���� $��1X���� $��1.25X���� $
��1.5X���� $��1.75X���� $��2X���� $��0,50,181,118q�$��
Run Speed:��$��0��$��1���� $��94,94,33,87q�$��1��$ 3��.@��� $��0.25X��$
��Left=�$	��146��$��2,0,-2,0q��� $
��0.5X���� $��0.75X��$
��Left=�$	��146���� $��1X���� $��1.25X���� $
��1.5X���� $��1.75X���� $��2X���� $��0,92,181,76q�$��Jump Height:��$��0��$��1���� $	��Map��.���  �� $��20 ��� $��* ��� $	��200 � �� $��20 ��� $��* ��� $��30 �.+��� -   $��MapTiles��$��2��$��
0,10,10,10q�$��1��$��Disabled=�$��Visible=�$��Right=�$	��180������ .R��  � Orientation$ 
Horizontal=��� -   $��Mapping��$��1��$��10q�$��1�����	#FFFFC5C5.+�� -   $��gridMapTiles��$	��349��$	��500���� -   $��txtGridWidth��$��10��$��1��$
��Left=�$��26��$��
135,5,0,-1q�$��2��$
 Wrap=�$	��Top=�$��38���� $��	28,6,0,-1q�$��2��$��
Map Width:��$
��Left=�$	��Top=�$��25��$��1���� $��
Background��.���  �� $��20 ��� $��* ��� $	��200 � �� $��20 ��� $��* ��� $��30 �.+��� 
���   �$��1q�$��1��$��10,10,10,12q�$��1��._��� -   $��
background��$��1��$��8,8,9,9q�$��1���� -   $��loadBack��$��2��$��1��$��
0,10,0,327q�$��
Load Image���� -   $��saveBack��$��2��$��1��$��
0,47,0,290q�$��
Save Image��