   M S B A M L   `   `   `  ���� N  JVirtualDressingRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VirtualDressingRoomVirtualDressingRoom  $    VirtualDressingRoom.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:VirtualDressingRoom       � Title$  
MainWindow��$��2098.826��$��2050.565�� � 	Resources 1  +VirtualDressingRoom.CategoryButtonViewModel   CategoryButtonViewModelKey0  *VirtualDressingRoom.ClothesButtonViewModel  ClothesButtonViewModelKey'  !VirtualDressingRoom.HairViewModel  HairViewModelKey+  %VirtualDressingRoom.NecklaceViewModel  NecklaceViewModelKey&   VirtualDressingRoom.TopViewModel  TopViewModelKey)  #VirtualDressingRoom.BottomViewModel  BottomViewModelKey,  &VirtualDressingRoom.ShoesLeftViewModel  ShoesLeftViewModelKey-  'VirtualDressingRoom.ShoesRightViewModel  ShoesRightViewModelKey��%x   &	        &	      &	 
     &	      &	      &	      &	      &	 #                     .��� .+��� -   $��canvas1��.+��� -   $��Image1��$
��Left=�$
��2057��$
��Fill=�$��0,0,0,0q�$	��Top=�$
��2024�� K� ZIndex$ -1���� -   $��Image2��$
��Left=�$��43��$
��Fill=�$	��Top=�$
��2034��$@��:D:/VirtualDressingRoom/VirtualDressingRoom/image/title.png�� J� Opacity$	 0.5Q�$ 0���� -    ��Name$	 Buy��$
��Left=�$��	344,0,0,0q�$	��Top=�$��75��$��40�� J� OpacityMask
 �����.����  �� Orientation$ 
Horizontal=�$
��Auto��$
��Left=�.+��� $B��<D:/VirtualDressingRoom/VirtualDressingRoom/image/img_buy.png��
 ������� -   $ Capture��$
��Left=�$��	434,0,0,0q�$	��Top=�$��75��$��40��
 �����.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� $E��?D:/VirtualDressingRoom/VirtualDressingRoom/image/img_camera.png��
 ������� -   $ Category_down��$
��Left=�$��45��$��	54,85,0,0q�$	��Top=�$	��150��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� $A��;D:/VirtualDressingRoom/VirtualDressingRoom/image/img_up.jpg��
 ������ -   $��category�� � DataContext ��  �� Source# [  .+��� -   $ 	Category1��$
��Left=�$	��150��$��
54,150,0,0q�$	��Top=�$	��150��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� 	 �� Path$	 Path1�
 �� Mode$
 TwoWay=��� -	   $ 	Category2��$
��Left=�$	��150��$��
54,320,0,0q�$	��Top=�$	��150��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path2�$
 TwoWay=��� -
   $ 	Category3��$
��Left=�$	��150��$��
54,490,0,0q�$	��Top=�$	��150��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path3�$
 TwoWay=��� -   $ Category_up��$
��Left=�$��45��$��
54,660,0,0q�$	��Top=�$	��150��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� $C��=D:/VirtualDressingRoom/VirtualDressingRoom/image/img_down.jpg��
 ������� -   $ Clothes_down��$��Right=�$��45��$��	0,55,54,0q�$	��Top=�$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� $A��;D:/VirtualDressingRoom/VirtualDressingRoom/image/img_up.jpg��
 ������ -   $��clothes�� �� # [ .+��� -   $ Clothes1��$��Right=�$��
0,120,54,0q�$	��Top=�$	��130��$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path1�$
 TwoWay=��� -   $ Clothes2��$��Right=�$��
0,270,54,0q�$	��Top=�$	��130��$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path2�$
 TwoWay=��� -   $ Clothes3��$��Right=�$��
0,420,54,0q�$	��Top=�$	��130��$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path3�$
 TwoWay=��� -   $ Clothes4��$��Right=�$��
0,570,54,0q�$	��Top=�$	��130��$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� 
 ��������� $	 Path4�$
 TwoWay=��� -   $ 
Clothes_up��$��Right=�$��45��$��
0,720,54,0q�$	��Top=�$	��130��.���� $ 
Horizontal=�$
��Auto��$
��Left=�.+��� $C��=D:/VirtualDressingRoom/VirtualDressingRoom/image/img_down.jpg��
 ������� -   $��Z��$
��Left=�$��1023,90,0,0q�$
��Wrap=�$��	TextBlock��$	��Top=�$��55��$	��522��$��25(�� -   $
��Hair��$
��Left=�$	��200��$��740,680,0,0q�$	��Top=�$	��200�� �� # [ .+��� $
��Left=�$	��200��$��0,0,0,0q�$	��Top=�$	��200������ $
	 Path�$
 TwoWay=�� -   $��Necklace��$
��Left=�$	��200��$	��Top=�$	��600��$ 3�� �� # [ .+��� $
��Left=�$	��200��$��0,0,0,0q�$	��Top=�$	��600������ $
	 Path�$
 TwoWay=�� -   $��
ClothesTop��$
��Left=�$	��400��$	��Top=�$	��650�� �� # [ .+��� -   $��ImgClothesTop��$
��Left=�$	��400��$��0,0,0,0q�$	��Top=�$	��650������ $
	 Path�$
 TwoWay=�� -   $��ClothesBottom��$
��Left=�$	��700��$��
877,80,0,0q�$	��Top=�$	��600�� �� # [ .+��� -   $��ImgClothesBottom��$
��Left=�$	��700��$��0,0,0,0q�$	��Top=�$	��600������ $
	 Path�$
 TwoWay=�� -   $��	ShoesLeft��$
��Left=�$	��100��$��484,490,0,0q�$	��Top=�$	��100�� �� # [ .+��� $
��Left=�$	��Top=�$��0,0,0,0q�$	��100��$	��100������ $
	 Path�$
 TwoWay=�� -   $��
ShoesRight��$
��Left=�$	��100��$��319,605,0,0q�$	��Top=�$	��100�� �� # [ .+��� $
��Left=�$	��Top=�$��0,0,0,0q�$	��100��$	��100������ $
	 Path�$
 TwoWay=�