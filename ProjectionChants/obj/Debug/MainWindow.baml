   M S B A M L   `   `   `  ����K  GProjectionChants, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ProjectionChantsProjectionChants  5   	   !    ProjectionChants.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ProjectionChants     5   	   6tk-http://schemas.microsoft.com/wpf/2008/toolkit  5   	   -   5          �Name$  
mainWindow��5   B   $	��768��6�   $
��1024��6�    � WindowState$ 	Maximized=�6�      myDataContext � DataContext# �   6�   $��ExtraExpanded&�6�    � UseLayoutRounding .  $��20(�6#   � Icon$ projection.ico��6L  $
��1024��6�  ���	#FFC7D2E4 � Topmost .  � 	Resources 5      .   (ProjectionChants.InverseBooleanConverter  InverseBoolConverter&    ProjectionChants.UserDataContext3   -ProjectionChants.BooleanToVisibilityConverter   booleanToVisibilityConverter  closeCommands �� 
TargetName �� TargetProperty		 W� To
 g� Duration  openCommands��%a  &	       &	       &	 &     &	 9     &	 �       5	   
   6(     5
   
   6      5   
   6-   �� 5   
   .��5      W� $ transformCommandScale��6   $ ScaleY�6L   $	 0X�6o   $
 0:0:0.5D�6v   5      �� 5   
   .��5      W� $ transformCommandScale��6   $ ScaleY�6L   $	 1X�6o   $
 0:0:0.5D�6v   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��337* �6   $	��300��6,   �� 5      $
��Auto �6   �� 5      $
��627* �6   �� 5      $��0 �6    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      .+�5   
   )  #Microsoft.Windows.Controls.DataGrid  -   5          Name$ 
gridChants��5      -  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 Once=�6*      AutoGenerateColumns .   J� RenderTransformOrigin$ 0.5,0.5&�6n   $��1��6�   $��0,0,0,0q�6�   $��16(�6�   $��Italic$�6�      
IsReadOnly .    RowBackground
 �����   AlternatingRowBackground �	#FFF1F1F1 �� SelectedIndex$ 0��6     CanUserResizeRows .     SelectionMode$ Single=�6}   �� IsTextSearchEnabled . ��. $��4��6�  ��6�  Z� ��6   Z�  5        SelectableCellStyle �� 
TargetType-  'Microsoft.Windows.Controls.DataGridCell �� Property �� Value �� Triggers P� Property P� Value��%�   &	       �� 5       "  65   .�5!      ��  �@ 	IsTabStop .  6E    5"      P� 5#       �@ 
IsSelected . . �5$      ��  �@ 	IsTabStop . 6M   5%      5'      5(         HorizontalGridLinesBrush 5)      �� 5*       �� Color# � ��6"       VerticalGridLinesBrush  5,      �� 5-      # � ��6"   {�5/      �� 50      ! �� 	Converter#! [ 6   " �� Path$" modeEdition�6L   #   	CellStyle# 52      �� 53      "  6   .�54      ��  �@ 	IsTabStop .  6E   55      $   Columns$ 57      3  -Microsoft.Windows.Controls.DataGridTextColumn  58      -   5        
numero��/  )Microsoft.Windows.Controls.DataGridColumn%   Header$	% N°��58   v   &   Width8	  2Microsoft.Windows.Controls.DataGridLengthConverter$& 40	 6�   '   	CellStyle#' �  6�   4
  .Microsoft.Windows.Controls.DataGridBoundColumn( 
  Binding( 68   �� *numero+) 
  ClipboardContentBinding) 6S   Z� 6�     59      -   5        nom��$% Titre��59   p   $& *	 6   ( 65   �� *nom+) 6M   Z� 6     5:      -   5        	theme��$% Thème��5:   t   $& 85	 6�   ( 67   �� *theme+) 6Q   Z� 6�     5;      -   5        cle��$
% Clé��5;   p   $& 35	 6}   ( 65   �� *cle+) 6M   Z� 6}   5=      �� 5>   
   -   5        * ��Name$* stackCommand��5>      $��0q�6+   $��1��66   $	��Top=�6C   
���  ��$	 0,0&�6l   $	��170��6�   z�5?      V� 5@      .~�5A      �� -   5        transformCommandScale��5A   %   �� 5B      �� 5C      T� 5D      5E      .+�5G      �� + �� Orientation$+ 
Horizontal=�6   $��60��62   $��Center=�6>   .+�5H      �� -	   5        , ��Name$, 
buttonSave��5H      - � ToolTip$- Enregistrer��6-   ��.  $��Center=�6n   .��5I      �� $��Images/save.png��6   $��32��65   $��32��6@   . � Triggers. 5J      :� 5K      / :� RoutedEvent$/ Button.Click��6'   .[�5L      �� #��[ 6.   5M      5O      �� 5P      -
   5        $, buttonCancel��5P      $- Annuler��6/   ��.  $��Center=�6n   $��20,0,0,0q�6�   .��5Q      �� $��Images/round_delete.png��6   $��32��6=   $��32��6H   . 5R      :� 5S      $/ Button.Click��6'   .[�5T      �� #��[ 6.   5U      5W      �� 5X      -   5        $, buttonDelete��5X      $- 	Supprimer��6/   ��.  $��Center=�6U   $��20,0,0,0q�6p   .��5Y      �� $��Images/trash.png��6   $��32��66   $��32��6A   . 5Z      :� 5[      $/ Button.Click��6'   .[�5\      �� #��[ 6.   5]      5_      5`      �� 5a      $��9��6   $��0q�6#   �� 5b      $+ 
Horizontal=�6   $��Center=�62   $
��0,10q�6O   .+�5c      �� $��32��6   $��32��6$   $��Images/text_letter_t.png��6/   �� 5d      -   5        0 ��Name$0 _fontFamily��5d      1 �� 
IsEditable1 . $	��200��6q   �� 5e      $��32��6   $��32��6$   $��Images/font_size.png��6/   $��10,0,0,0q�6M   �� 5f      -   5        $0 	_fontSize��5f      $��50��6.   1 . 6K   5g      �� 5h      $+ 
Horizontal=�6   $��Center=�62   $��0,0,0,10q�6O   .+�5i      d� -   5        2 d�Name$2 _btnBold��5i      .  (System.Windows.Documents.EditingCommands3   
ToggleBold#��Z3 61   ��6a   �� *SelectedRtb+.��5j      �� $��Images/font_bold.png��6   $��32��6:   $��32��6E   5k      d� 5l      -   5        $2 
_btnItalic��5l      4   ToggleItalic#��Z4 63   ��6e   �� *SelectedRtb+.��5m      �� $��Images/font_italic.png��6   $��32��6<   $��32��6G   5n      d� 5o      -   5        $2 _btnUnderline��5o      5   ToggleUnderline#��Z5 66   ��6k   �� *SelectedRtb+.��5p      �� $��Images/font_underline.png��6   $��32��6?   $��32��6J   5q      d� 5r      -   5        $2 _btnBullets��5r      6   ToggleBullets#��Z6 64   $��10,0,0,0q�6�   ��6g   �� *SelectedRtb+.��5s      �� $��Images/list_bullets.png��6   $��32��6=   $��32��6H   5t      d� 5u      -   5        $2 _btnNumbers��5u      7   ToggleNumbering#��Z7 64   ��6i   �� *SelectedRtb+.��5v      �� $��Images/list_num.png��6   $��32��69   $��32��6D   5w      d� 5x      -   5        $2 _btnAlignLeft��5x      8   	AlignLeft#��Z8 66   $��10,0,0,0q�6�   ��6e   �� *SelectedRtb+.��5y      �� $��Images/align_left.png��6   $��32��6;   $��32��6F   5z      d� 5{      -   5        $2 _btnAlignCenter��5{      9   AlignCenter#��Z9 68   ��6i   �� *SelectedRtb+.��5|      �� $��Images/align_center.png��6   $��32��6=   $��32��6H   5}      d� 5~      -   5        $2 _btnAlignRight��5~      :   
AlignRight#��Z: 67   ��6g   �� *SelectedRtb+.��5      �� $��Images/align_right.png��6   $��32��6<   $��32��6H   5�      5�      5�      �� 5�   
   $��1��6   $��0,0,0,0q�6'   $��1��68   $��5��6E   ; �� ResizeBehavior$; PreviousAndNext=�6O   ��.  ���	#FFC7D2E46�   �� 5�   
   $��3��6   $+ 
Horizontal=�6)   $��0q�6B   $
��Left=�6M   $	��890��6h   .+�5�      �� -   5        $, ajouterChant��5�      $��Center=�6�   $- Ajouter un chant��6�   ��.  $��10,0,0,0q�6�   {�6F   �� *modeEdition+#! [ .��5�      �� $��Images/round_plus.png��6   $��32��67   $��32��6C   < J� Effect< 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�      =   Opacity$	= 0.5Q�6'   . 5�      :� 5�      $/ Button.Click��6#   .[�5�      �� #��[ 6*   5�      5�      �� 5�      -   5        $, modifierChant��5�      $��Center=�6�   $- Modifier ce chant��6�   ��.  $��5,0,0,0q�6�   {�6H   �� *modeEdition+#! [ .��5�      �� $��Images/pencil.png��6   $��32��63   $��32��6?   < 5�        5�      $	= 0.5Q�6'   . 5�      :� 5�      $/ Button.Click��6#   .[�5�      �� #��[ 6*   5�      5�      �� 5�      -   5        > ��Name$> 
textNumero��5�      $��50��6*   $��35��65   $
��Bold"�6A   $��Italic$�6S   $��Center=�6f   $��0��6�   $��10,0,0,0q�6�   ��6�   �� *Numero+.��6	  < 5�        5�      $	= 0.5Q�6'   .��5�   "   5�      �� 5�      -   5        $> 	textTitre��5�      $	��380��6)   $��35��65   $
��Bold"�6A   $��Italic$�6S   $��Center=�6f   $��1��6�   ��6�   �� *Titre+.��6�   < 5�        5�      $	= 0.5Q�6'   .��5�   "   5�      �� 5�      $��Thème :��6   $��10,0,0,0q�6&   $
��Bold"�68   $��Italic$�6J   $��Center=�6]   < 5�        5�      $	= 0.5Q�6'   5�      �� 5�      -   5        $0 
comboTheme��5�      $	��165��6+   $
��Bold"�67   $��Italic$�6I   1 . $? �� IsSynchronizedWithCurrentItem$
? TrueY�6n   $��Center=�6�   $��35��6�   ��. $��2��6  @ �� Text@ 6�   �� *Theme+< 5�        5�      $	= 0.5Q�6'   5�      �� 5�      $��Clé :��6   $��10,0,0,0q�6$   $
��Bold"�66   $��Italic$�6H   $��Center=�6[   < 5�        5�      $	= 0.5Q�6'   5�      �� 5�      -   5        $> textCle��5�      $��40��6'   $
��Bold"�62   $��Italic$�6D   $��Center=�6l   $��35��6�   $��3��6�   ��6W   �� *Cle+.��6�   < 5�        5�      $	= 0.5Q�6'   .��5�   "   5�      5�      �� 5�   
   $��2��6   $��Right=�6%   $+ 
Horizontal=�6A   $��2��6Z   .+�5�      o� -   5        A o�Name$A menuOptions��5�      B o� 
IsMainMenuB .  $��Center=�6;   < 5�        5�      $	= 0.5Q�6'   .@�5�      m� $
��Menu��6   .@�5�      m� -   5        C m�Name$C menuChooseList��5�      $��Choisir une liste��67   6R   �� 5�      m� 5�      -   5        $C menuStopProjection��5�      $��Arrêter la projection��6;   ��.  D m� IsCheckableD . 6�   m� 5�      -   5        $C menuShowTab��5�      $��Afficher les tablatures��64   ��.  D . E m� 	IsCheckedE 6z   �� *isTabVisible+m� 5�      -    5        $C menuPrintList��5�      $��Imprimer la liste��66   ��.  6c   5�      5�      �� 5�      -!   5        $, buttonProjection��5�      $��Center=�6/   $��1q�6J   $
��Bold"�6^   $��Italic$�6p   $��5��6�   $��38��6   $
��10,0q�6+  {�6�   �� *	IsChecked+#! [ F �� ElementName$F menuStopProjection��< 5�        5�      $	= 0.5Q�6'   .��5�      �� $��Images/preso.png��6   $��32��62   $��32��6=   5�      5�      (� 5�   
   -"   5        G (�Name$G viewParoles��5�      $��1��6'   $��2��64   .��5�      � -#   5        H �Name$H gridParoles��5�      $
��1024��6(   $	��768��65    .  5�      �� 5�      ��6$   �� *RefrainLength+I �� Mode$I TwoWay=���6R   �� *Parent.Height+J �� RelativeSourceJ �� *Self+�� 5�      $
��Auto �6$   �� 5�      $��* �6$   .+�5�      � -$   5        $H gridRefrain��5�       .  5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   �� 5�      ��6+   �� *RefrainTabLength+$I TwoWay=���6[   �� *Parent.Width+J �� *Self+.+�5�      �� -%   5        K ��Name$K 
rtbRefrain��5�   "   L �� 
AcceptsTabL . $��20(�6H   "M �� VerticalScrollBarVisibility$
M Auto=�6V   $N �� HorizontalScrollBarVisibility$
N Auto=�6y   $��6��6�   $��0q�6:  |�6�   �� *modeEdition+O �� 	IsEnabledO 6  �� *modeEdition+. �5�      /� P � Language$P fr-fr�6'   Q /� 
LineHeight$Q 1��68   .T�5�      J� 5�      5�      �� 5�      -&   5        R ��Name$R rtbRefrainSplitter��5�   #   $��1��6?   $��Stretch=�6O   $��5��6m   ��.  ���	#FFC7D2E4{�6�   �� *modeEdition+y�6�   �� *modeEdition+#! [ �� 5�      -'   5        $K rtbRefrainTab��5�   "   $��2��69   L . $��Normal&�6[   $��20(�6p   $
M Auto=�6~   $
N Auto=�6�   $	 0,0&�6�   $��0q�6�   $��6��6�   |�6  �� *modeEdition+O 6e  �� *modeEdition+. �5�      /� $P fr-fr�6'   $Q 1��68   .T�5�      J� 5�      5�      5�      �� 5�      -(   5        $R rtbSplitter��5�      $��Stretch=�64   $��5��6R   ��.  ���	#FFC7D2E4$��1��6  {�6�   �� *modeEdition+y�6�   �� *modeEdition+#! [ 6  � 5�      -)   5        $H gridCouplet��5�      $��2��6,    .  5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   �� 5�      ��6+   �� *CoupletTabLength+$I TwoWay=�.+�5�      �� -*   5        $K 
rtbCouplet��5�   "   L . $��Normal&�6u   $��20(�6�   ���	#FFD48F8F$
M Auto=�6�   $
N Auto=�6�   ��. $	 0,0&�6  $��0q�69  $��7��6h  O 6H   �� *modeEdition+|�6u  �� *modeEdition+. �5�      /� $P fr-fr�6'   $Q 1��68   .T�5�      J� 5�      5�      �� 5�      -+   5        $R rtbCoupletSplitter��5�   #   $��1��6?   $��Stretch=�6O   $��5��6m   ��.  ���	#FFC7D2E4{�6�   �� *modeEdition+y�6�   �� *modeEdition+#! [ �� 5�      -,   5        $K rtbCoupletTab��5�   "   $��2��69   L . $��Normal&�6�   $��20(�6�   ���	#FFD48F8F$
M Auto=�6�   $
N Auto=�6�   $	 0,0&�6  $��0q�6'  $��7��6;  O 6[   �� *modeEdition+|�6H  �� *modeEdition+. �5�      /� $P fr-fr�6'   $Q 1��68   .T�5�      J� 5�      5�      5�      5�      5�      5�      5�      