   M S B A M L   `   `   `  ����K  GProjectionChants, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ProjectionChantsProjectionChants  5   @   '    !ProjectionChants.ProjectionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	   0localclr-namespace:ProjectionChants     6@   -   5          �Name$  
Projection��5   m   $	��400��6�   $	��400��6�    � WindowStyle$
 None=�6�   .��5   
   (� -   5         (�Name$ viewProjection��5      .��5   
   � -   5         �Name$ gridParoles��5      $
��1024��6$   $	��768��61    � UseLayoutRounding .  � RowDefinitions 5      �� 5	      -   5         ��Name$ 
rowRefrain��5	       �� 5
      .+�5      � -   5        $ gridRefrain��5       .  � ColumnDefinitions 5      �� 5      �� 5      -   5         ��Name$ columnRefrainTab��5   '   .+�5      �� -   5        	 ��Name$	 
rtbRefrain��5      $��0q�62   ��. . �5      /� 
 � Language$
 fr-fr�6#    /� 
LineHeight$ 1��64   .T�5      J� 5      5      �� 5      -   5        $	 rtbRefrainTab��5      $��1��65   $��0q�6E   . �5      /� $
 fr-fr�6#   $ 1��64   .T�5      J� 5      5      5      � 5      -	   5        $ gridCouplet��5      $��1��6(    .  5      �� 5      �� 5      -
   5        $ columnCoupletTab��5   '   .+�5!      �� -   5        $	 
rtbCouplet��5!      $��0q�62   ��. . �5"      /� $
 fr-fr�6#   $ 1��64   .T�5#      J� 5$      5%      �� 5&      -   5        $	 rtbCoupletTab��5&      $��1��65   $��0q�6E   . �5'      /� $
 fr-fr�6#   $ 1��64   .T�5(      J� 5)      5*      5+      �� 5,      -   5         ��Name$ blackRectangle��5,      
���   �$��	0,0,-0,-0q�6=   $y�Hidden=�6P   $
��2000��6d   $
��2000��6q   $��2��6   �� 5-      -   5        $ whiteRectangle��5-      
�������$��	0,0,-0,-0q�6=   $y�Hidden=�6P   $
��2000��6d   $
��2000��6q   $��2��6   5.      5/      51      