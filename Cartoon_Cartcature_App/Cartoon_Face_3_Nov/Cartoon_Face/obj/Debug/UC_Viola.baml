   M S B A M L   `   `   `  ����G  CCartoon_Face, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Cartoon_FaceCartoon_Face  5          Cartoon_Face.UC_Viola   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Cartoon_Face     5      .��5	      ]� .+�5
   	   �� -   5          ��Name$  	borderTop��5
      $��0��6#   $��Bottom=�60   $
��auto��6H   $
��Left=�6U   $
��auto��6p      OliveYellowGradientBrush#��[  6~   $��2q�6�    �� CornerRadius$ 5,5,5,5��6�   $��2q�6�   ._�5      �  � Orientation$ 
Horizontal=�6   $
��auto��62   .+�5      �� -   5         ��Name$	 grp��5      $��Lee's��6(   $��5q�67   .��5      � $��5q�6    � RowDefinitions 5      �� 5      $��* �6,   �� 5      $��* �6,   �� 5      $��* �6,    � ColumnDefinitions 5      �� 5      $��70 �6/   �� 5      $	��150 �6/   �� 5      $��70 �6/   �� 5      $��0* �6/   �� 5      .+�5      �� $��Alpha��6$   $��0��61   $��1��6A   #��[  6N   $��2,15,1.778,0.111q�6�   �� 5      -   5        $��siAlpha��5   !   $��1��60   $��1��6@    � Maximum$ 4Q�6M    � Minimum$ -2Q�6Y    �� TickFrequency$	 0.1Q�6f   	 �� TickPlacement$	 BottomRight=�6z   
 �� IsSnapToTickEnabled
 . $��0.222,15,0,0.111q�6�   $��2��6�   �� 5      $��2��6$   $��1��64   $��21,15,19.778,0.111q�6�   $��2��6�   ��6A   ��  �� ElementName$ siAlpha�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $
��Beta��6$   $��0��60   $��2��6@   #��[  6M   $��2,15,1.778,0.111q�6�   �� 5      -   5        $��siBeta��5   !   $��1��6/   $��2��6?   $ 8Q�6L   $ -2Q�6X   $	 0.1Q�6e   $	 BottomRight=�6y   
 . $��0.222,15,0,0.111q�6�   $��2��6�   �� 5       $��2��6$   $��2��64   $��21,15,19.778,0.111q�6�   $��2��6�   ��6A   �� $ siBeta��$ Value�$ PropertyChanged=�6�   5"      5#      �� 5%      -   5        $��
btnProceed��5%      $��Proceed��6+   $��Center=�6=   $��Center=�6Z   $	��100��6u     	blueFatin#��[ 6�   6�   5&      5'      �� 5(   
   $��2q�6   $��Center=�6   $
��auto��6<   $
��auto��6J   #��[  6X   $��2,2,2,2q�6�   $ 5,5,5,5��6�   ._�5)      �� $
��auto��6   $��RightToLeft=�6-   .+�5*      �� $��Original��6   �� 5+      -   5        $��Original��5+      $	��400��6(   $	��400��65   6C   5,      5-      �� 5.   
   $
��auto��6   $��RightToLeft=�6)   .+�5/      �� $��LIP Enhamced Image��6   �� 50      -   5        $��Enhanced��50      $	��400��6$   $	��400��61   6?   51      �� 52   
   $
��auto��6   $��RightToLeft=�6)   .+�53      �� $
��Rect��6   �� 54      -   5        $	��Rec��54      $	��400��6   $	��400��6,   6:   55      56      57      