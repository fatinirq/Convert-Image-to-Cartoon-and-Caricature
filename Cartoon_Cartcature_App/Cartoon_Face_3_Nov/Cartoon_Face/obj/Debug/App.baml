   M S B A M L   `   `   `  ����G  CCartoon_Face, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Cartoon_FaceCartoon_Face  5          Cartoon_Face.App   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:Cartoon_Face     5        �� 	Resources  5         	blueFatin 
� RadiusY 
� RadiusX 
� GradientOrigin � Color � Offset  OliveYellowGradientBrush �� 
StartPoint �� EndPoint  DisabledForegroundBrush �� Color   MyGlassGradientStopsResource  MyGlassBrushResource	 �� Opacity  LabelForeColor  GrayBlueGradientBrush ! GrayBlueGradientBrush_Enabled  borderstyle 	 EnabledButtonBackground
 �� 
TargetType �� Property �� Value �� BasedOn 
 	TitleText �� 
TargetType ��Name �� RadiusX �� RadiusY J� Opacity J� RenderTransformOrigin J� BitmapEffect ]�Name ��Name �� Triggers P� Property P� Value �� 
TargetName �� Radius :� RoutedEvent ��Name �� 
TargetName �� TargetProperty	  W� By! g� Duration" �� BeginStoryboardName��%z  &	        &	 �     &	 }    &	 �    &	 �    7  &	 1    &	 �    &	 	    &	 �    &		 �    '�� h    &	
     7��7  '�� x    7  7  7  
� 5      $ 0.573Q�68   $ 0.667Q�6H   $ 
1.01,1.035&�6X   .��5	      � $ 	#FF06183C��6#   $
 0.81Q�65   � 5
      $ 	#FF0D60A3��6#   5      �� 5      $	 0,0&�5      $	 1,1&�6   .��5      � $ DarkOliveGreen��6#   $ 0Q�6<   � 5      $ Bisque��6#   $	 0.5Q�62   � 5      $ DarkOliveGreen��6#   $ 1Q�6:   5      �� 5      $
 #888��6B   � 5      � 5      $ 	LightBlue��6#   $	 0.2Q�66   � 5      $ Transparent��6#   $	 0.4Q�67   � 5      $ 	LightBlue��6#   $	 0.5Q�65   � 5      $ Transparent��6#   $
 0.75Q�67   � 5      $ 	LightBlue��6#   $	 0.9Q�66   � 5      $ Transparent��6#   $ 1Q�67   5      �� 5      $	 0,0&�5      $	 1,1&�6   $
	 0.75Q�6(   8��  5      
� 5      $ 0.667Q�6=   $ 0.573Q�6M   $ 
1.01,1.035&�6]   .��5       � $ 	#FF06183C��6#   $
 0.81Q�65   � 5!      $ 	#FF0D60A3��6#   5"      �� 5#      $	 0,0&�5$      $	 1,1&�6   .��5%      � $ 	LightBlue��6#   $ 0Q�65   � 5&      $ #CCCCFF��6#   $
 0.25Q�63   � 5'      $ 	LightBlue��6#   $	 0.5Q�65   � 5(      $ #CCCCFF��6#   $
 0.75Q�63   � 5)      $ 	LightBlue��6#   $ 1Q�65   5*      �� 5+      $	 0,0&�5,      $	 1,1&�6   .��5-      � $ DarkOliveGreen��6#   $ 0Q�6<   � 5.      $ Bisque��6#   $	 0.5Q�62   � 5/      $ DarkOliveGreen��6#   $ 1Q�6:   50      
� 51      $ 0.573Q�6;   $ 0.667Q�6K   $ 
1.01,1.035&�6[   .��52      � $ 	#FF06183C��6#   $
 0.81Q�65   � 53      $ 	#FF0D60A3��6#   54      
� 56      $ 0.573Q�6F   $ 0.667Q�6V   $ 
1.01,1.035&�6f   .��57      � $ 	LightBlue��6#   $ 0Q�65   � 58      $ #CCCCFF��6#   $	 0.5Q�63   � 59      $ 	LightBlue��6#   $ 1Q�65   5:      �� 5;      "
 ��6   .�5<      ��  � ��$ Center=�6<   �� 5=       � ��$ Comic Sans MS)�63   �� 5>       � ��$ 14(�61   5?      �� 5A      8   "
 ��5B      .�5D      ��  � ��$ 26(�61   �� 5E       � ��8  63   5F      �� 5G      "
 ��6   .�5I      ��  � ��8   5J   	   �� 5O       � �� 5P      �� 5Q      " ��6.   .R�5R   "   � #��z��6'   #��z��5S      }�. .+�5V   &   �� $ outerRectangle��60   $��Stretch=�5W      $��Stretch=�5X      #��z��5Y      $ 20��5Z      $ 20��6   $��5��6'   
������ 5[      �� 5^   &   $ innerRectangle��60   $��Stretch=�5_      $��Stretch=�5`      
������ $��20��5a      #��z��5b      $ 20��5c      $ 20��6   �� 5e   &   $ 	glassCube��60   $��Stretch=�6C   $��Stretch=�5f      $��2��5g      $ 10��6!   $ 10��6.   $ 0Q�6;   8�� $ 0.5,0.5&�5i      ��5j   *   �� 5k   .   $ 0.5,0&�6B   $ 0.5,1&�6U   .��5l   2   � $	 0.0Q�6?   $ 	LightBlue��6L   � 5m   2   $	 1.0Q�6?   $
 Gray��6L   5n   /   z�5s   *   V� 5t   .   .~�5u   2   �� �� 5v   2   5w   /    5|   *   �� 5}   .   5   '   ]� 5�   &   $! myContentPresenterDockPanel��60   .+�5�   *   �� $ myContentPresenter��6;   $��20q�6W   #��z��5�      
���   �62   5�   '   5�   #    5�   "   P� 5�   &    � {� . . �5�   *   �� $ outerRectangle��6A   8   � ��6]   5�   '   P� 5�   &    �@J�IsMouseOver . . �5�   *   �� $ outerRectangle��6D   # � ��5�       � ���� 5�   *   $ 	glassCube��6T    �@J�Opacity$ 1Q�6D   6T   �� 5�   *   $ myContentPresenter��6O    �@J�BitmapEffect 5�   .   �� 5�   2   $ 1Q�6C   5�   +   5�   '   P� 5�   &    �@J�	IsFocused . . �5�   *   �� $ 	glassCube��6O    �@J�Opacity$ 1Q�6D   6O   �� 5�   *   $ outerRectangle��6C   # � ��5�       � ���� 5�   *   $ 	glassCube��6N    �@J�Opacity$ 3Q�6D   6N   5�   '   :� 5�   &   $ Mouse.MouseEnter��63   .[�5�   *   �� $ mouseEnterBeginStoryboard��6:   .��5�   .   �� .��5�   2   W� $ 	glassCube��6B   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5�      $
  -0.1X�5�      $! 0:0:0.5D�6   W� 5�   2   $ 	glassCube��5�      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5�      $
  -0.1X�5�      $! 0:0:0.5D�6   5�   /   5�   +   5�   '   :� 5�   &   $ Mouse.MouseLeave��63   .[�5�   *   �� $" mouseEnterBeginStoryboard��69   5�   '   :� 5�   &   $ ButtonBase.Click��63   .[�5�   *   �� .��5�   .   �� .��5�   2   W� $ 	glassCube��6B   $V P(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)�5�      $	  360X�5�      $! 0:0:0.5D�6   5�   /   5�   +   5�   '   5�      5�      5�      5�      5�      