   M S B A M L   `   `   `  ����F  BTripLog.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TripLog.WpfTripLog.Wpf  5   	       TripLog.Wpf.EditPhoto   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TripLog.Wpf     5   	   -   5          � 
ResizeMode$  CanMinimize=�5        � ShowInTaskbar .   � Title$ 
Edit Photo��5	   	   $	��461��6   $	��903��6)    � WindowStartupLocation$ CenterOwner=�65    � 	Resources 5
         HeadingTextBlockStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��5      .�5      ��  � ��$ 16(�5      �� 5       � ��$
 Bold"�5      5      5      .��5      � $��
0,0,-6,106q�6    � RowDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5        	 ��Name$	 image_tripPhoto��5      $
��Left=�6+   $��10,62,0,-24q�6F   $	��413��6[   $��2��6g   �� 5   
   $��Center=�5   
   #��[  5   
   $��232,279,671,61q�5   
   $��0��5        ��6
   �� *Name+6    � 5!   
   $
��Left=�6   $	��303��6*   $��423,62,0,-24q�67   $	��Top=�6M   $	��466��6e   $��2��6q   
 � ColumnDefinitions
 5"      �� 5#      $
��190* �6#   �� 5$      $
��197* �6#   .+�5&      �� $��Right=�5(      $��_Caption��5*      $��0,21,158,254q�6    �� Target 5)      ��  �� ElementName$ TextBox_Caption��5*      �� 5+      -   5         ��Name$ TextBox_Caption��5-      $��73,21,40,254q�5,      $��Center=�5.       �� 	MaxLength$	 100��5/      $��28��62   $��2��6>   $��14.667(�6R   $
��Bold"�6d   ��6   �� *Name+6d   �� 50      $��Right=�51      $��	_Location��52      $��0,73,156,206q�6   �� 53      -   5        $ Textbox_Location��53      $��Stretch=�54      $��73,73,28,204q�55   %   $��14.667(�6;   $
��Bold"�6M   ��6   �� *
Location+6M   �� 56      $��0,72,172,204q�57      $��Right=�58      $��_Date��5:      $��1��6    59      �� $ DatePicker_PhotoDate��5:      (  "System.Windows.Controls.DatePicker  5;      -   5          Name$ DatePicker_PhotoDate��5>      $��66,72,0,203q�5=      $��Center=�5?      $��28��6V   $��1��6b   $
��Left=�6r   $	��131��6�   $��14.667(�6�   $
��Bold"�6�      SelectedDate 6&   �� *	PhotoDate+ �� Mode$ TwoWay=�6�   �� 5@      $��0,124,156,154q�5A      $��Right=�5B      $��
File _Name��5D       5C      �� $ TextBox_FileName��5D      �� 5E      -   5        $ TextBox_FileName��5F      $��Center=�5H      $��73,125,29,150q�6&   $��28��6=   $��14.667(�6I   $
��Bold"�6[   ��5G      �� *ExportFileName+5H   [   �� 5I      $��0,123,170,153q�5J      $��Right=�5K      $��
File _Type��5M      $��1��6     5L      �� $ TextBox_FileType��5M       �� 5N      -   5        $ TextBox_FileType��5O       �� 
IsReadOnly . $��Center=�5R      $��67,125,40,150q�6&   $��28��6=   $��1��6I   $��14.667(�6Y   $
��Bold"�6k   ��5Q      �� *PhotoFormat+$ OneWay=�5R   k   �� 5S      $��Right=�5T      $��	_Latitude��5U      $��0,179,155,94q�6   �� 5V      -   5        $ textbox_latitude��5V      $��Stretch=�5W      &    TripLog.Wpf.TextBoxNumericalMask   Mask$ Decimal=�6)   $��73,178,28,97q�5X   %   $��14.667(�6;   $
��Bold"�6M   ��6   �� *
Latitude+6M   �� 5Y      $��Right=�5Z      $��
_Longitude��5[      $��0,176,169,97q�6    $��65��66   $��1��6A   �� 5\      -	   5        $ textbox_longitude��5\      $��Right=�5]      $ Decimal=�6'   $��0,178,40,97q�5^   &   $��1��6;   $	��130��6K   $��14.667(�6W   $
��Bold"�6i   ��6   �� *	Longitude+6i   �� 5_      $��0,225,156,40q�5`      $��Right=�5a      $��_Description��5c      $��69��6"    5b      �� $ TextBox_Description��5c   "   �� 5d      -
   5        $ TextBox_Description��5e      $��Stretch=�5g      $��73,228,40,45q�6'   $��2��6=   $��14.667(�6Q   $
��Bold"�6c   ��5f      �� *Description+$ TwoWay=�5g   c   5h      �� 5i   
   -   5         ��Name$ label_tripName��5i      $	��Top=�6(   $��
423,25,0,0q�6�   $	��Top=�6�   $��Right=�6�   $	��422��6�    J� SnapsToDevicePixels . $��56��6  ���	#FF2F9399$��24(�67  $
��Bold"�6E  $
��Left=�6W  $��Segoe Print)�6r  ��6G   �� *SelectedItem.Name+$ listBox_trips����6�   Z�  J� BitmapEffect 5j      F� 5k       F� Color$ LightSeaGreen��6)    F� 	Direction$	 200Q�6?    F� ShadowDepth$ 5Q�6O    F� Opacity$ .9Q�6_    F� Softness$ 8Q�6l   5m      �� 5n   
   -   5         ��Name$ button_updatePhoto��5n      $��_Update��6-   $
��Left=�6?   $��771,41.5,0,-73.5q�6Z   $	��Top=�6t   $��75��6�    .  �� 	IsDefault . $��32��6�   $��1��6�    � ToolTip$ Update Current Photo��6  ��6�   Z� 6  �� 5o   
   -   5        $ button_cancel��5o      $��_Cancel��6(   $
��Left=�6:   $��548,43.5,0,-75.5q�6U   $	��Top=�6o   $��75��6�    .  �� IsCancel . $��32��6�   $��1��6�   $ Cancel and Close��6  ��6�   Z� 6  �� 5p   
   -   5        $ button_changePhoto��5p      $
��Left=�6-   $��304,42,0,-74q�6H   $	��Top=�6^   $	��120��6v    .  . $��32��6�   $��1��6�   $ Select another photo��6�   ��6�   Z� .��5q      ��   �� Orientation$  
Horizontal=�6   .+�5r      �� $"��Resources\imageSelection.png��6   6@   �� 5s      $�� Change Photo��6   $��Center=�61   $ Select another Photo��6L   5t      5v      �� 5w   
   -   5        $ button_deletePhoto��5w      $��_Delete��6-   $
��Left=�6?   $��662,43.5,0,-75.5q�6Z   $	��Top=�6t   $��75��6�    .  . $��32��6�   $��1��6�   $ Delete Current Photo��6  ��6�   Z� 6  5y      5z      