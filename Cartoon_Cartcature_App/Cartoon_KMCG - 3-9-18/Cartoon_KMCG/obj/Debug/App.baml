   M S B A M L   `   `   `  ����G  CCartoon_KMCG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Cartoon_KMCGCartoon_KMCG  5          Cartoon_KMCG.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:Cartoon_KMCG     5        �� 	Resources  5         	blueFatin 
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
� 5   
   $ 0.573Q�60   $ 0.667Q�6@   $ 
1.01,1.035&�6P   .��5      � $ 	#FF06183C��6   $
 0.81Q�6-   � 5	      $ 	#FF0D60A3��6   5
      �� 5   
   $	 0,0&�5      $	 1,1&�6   .��5      � $ DarkOliveGreen��6   $ 0Q�64   � 5      $ Bisque��6   $	 0.5Q�6*   � 5      $ DarkOliveGreen��6   $ 1Q�62   5      �� 5   
   $
 #888��6:   � 5   
   � 5      $ 	LightBlue��6   $	 0.2Q�6.   � 5      $ Transparent��6   $	 0.4Q�6/   � 5      $ 	LightBlue��6   $	 0.5Q�6-   � 5      $ Transparent��6   $
 0.75Q�6/   � 5      $ 	LightBlue��6   $	 0.9Q�6.   � 5      $ Transparent��6   $ 1Q�6/   5      �� 5   
   $	 0,0&�5      $	 1,1&�6   $
	 0.75Q�6(   8��  5      
� 5   
   $ 0.667Q�65   $ 0.573Q�6E   $ 
1.01,1.035&�6U   .��5      � $ 	#FF06183C��6   $
 0.81Q�6-   � 5       $ 	#FF0D60A3��6   5!      �� 5"   
   $	 0,0&�5#      $	 1,1&�6   .��5$      � $ 	LightBlue��6   $ 0Q�6-   � 5%      $ #CCCCFF��6   $
 0.25Q�6+   � 5&      $ 	LightBlue��6   $	 0.5Q�6-   � 5'      $ #CCCCFF��6   $
 0.75Q�6+   � 5(      $ 	LightBlue��6   $ 1Q�6-   5)      �� 5*   
   $	 0,0&�5+      $	 1,1&�6   .��5,      � $ DarkOliveGreen��6   $ 0Q�64   � 5-      $ Bisque��6   $	 0.5Q�6*   � 5.      $ DarkOliveGreen��6   $ 1Q�62   5/      
� 50   
   $ 0.573Q�63   $ 0.667Q�6C   $ 
1.01,1.035&�6S   .��51      � $ 	#FF06183C��6   $
 0.81Q�6-   � 52      $ 	#FF0D60A3��6   53      
� 55   
   $ 0.573Q�6>   $ 0.667Q�6N   $ 
1.01,1.035&�6^   .��56      � $ 	LightBlue��6   $ 0Q�6-   � 57      $ #CCCCFF��6   $	 0.5Q�6+   � 58      $ 	LightBlue��6   $ 1Q�6-   59      �� 5:   
   "
 ��6   .�5;      ��  � ��$ Center=�64   �� 5<       � ��$ Comic Sans MS)�6+   �� 5=       � ��$ 14(�6)   5>      �� 5@   
   8   "
 ��5A      .�5C      ��  � ��$ 26(�6)   �� 5D       � ��8  6+   5E      �� 5F   
   "
 ��6   .�5H      ��  � ��8   5I   	   �� 5N       � �� 5O      �� 5P      " ��6&   .R�5Q      � #��z��6   #��z��5R      }�. .+�5U      �� $ outerRectangle��6(   $��Stretch=�5V      $��Stretch=�5W      #��z��5X      $ 20��5Y      $ 20��6   $��5��6'   
������ 5Z      �� 5]      $ innerRectangle��6(   $��Stretch=�5^      $��Stretch=�5_      
������ $��20��5`      #��z��5a      $ 20��5b      $ 20��6   �� 5d      $ 	glassCube��6(   $��Stretch=�6;   $��Stretch=�5e      $��2��5f      $ 10��6!   $ 10��6.   $ 0Q�6;   8�� $ 0.5,0.5&�5h      ��5i   "   �� 5j   &   $ 0.5,0&�6:   $ 0.5,1&�6M   .��5k   *   � $	 0.0Q�67   $ 	LightBlue��6D   � 5l   *   $	 1.0Q�67   $
 Gray��6D   5m   '   z�5r   "   V� 5s   &   .~�5t   *   �� �� 5u   *   5v   '    5{   "   �� 5|   &   5~      ]� 5�      $! myContentPresenterDockPanel��6(   .+�5�   "   �� $ myContentPresenter��63   $��20q�6O   #��z��5�      
���   �62   5�      5�       5�      P� 5�       � {� . . �5�   "   �� $ outerRectangle��69   8   � ��6U   5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ outerRectangle��6<   # � ��5�       � ���� 5�   "   $ 	glassCube��6L    �@J�Opacity$ 1Q�6<   6L   �� 5�   "   $ myContentPresenter��6G    �@J�BitmapEffect 5�   &   �� 5�   *   $ 1Q�6;   5�   #   5�      P� 5�       �@J�	IsFocused . . �5�   "   �� $ 	glassCube��6G    �@J�Opacity$ 1Q�6<   6G   �� 5�   "   $ outerRectangle��6;   # � ��5�       � ���� 5�   "   $ 	glassCube��6F    �@J�Opacity$ 3Q�6<   6F   5�      :� 5�      $ Mouse.MouseEnter��6+   .[�5�   "   �� $ mouseEnterBeginStoryboard��62   .��5�   &   �� .��5�   *   W� $ 	glassCube��6:   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5�      $
  -0.1X�5�      $! 0:0:0.5D�6   W� 5�   *   $ 	glassCube��5�      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5�      $
  -0.1X�5�      $! 0:0:0.5D�6   5�   '   5�   #   5�      :� 5�      $ Mouse.MouseLeave��6+   .[�5�   "   �� $" mouseEnterBeginStoryboard��61   5�      :� 5�      $ ButtonBase.Click��6+   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $ 	glassCube��6:   $V P(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)�5�      $	  360X�5�      $! 0:0:0.5D�6   5�   '   5�   #   5�      5�      5�      5�      5�      5�      