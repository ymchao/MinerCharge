   M S B A M L   `   `   `  ���� M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:MinerLampMangement.view.viewbean MinerLampMangement.view.viewbean  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    LinearGradientBlueBackground  �� EndPoint �� 
StartPoint � Color � Offset  MyBtnBorderColor �� Color  MyBtnHoverBackgroundColor  Textboxstyle �� 
TargetType �� Property �� Value  TextBlockstyle  MyWpfButton �� 
TargetType	 ��Name
 J� SnapsToDevicePixels �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName �� ScaleX �� ScaleY  DataGrid&    System.Windows.Controls.DataGrid)  #System.Windows.Controls.DataGridRow �� Triggers  DataGridColumnHeader=  7System.Windows.Controls.Primitives.DataGridColumnHeader  ExpanderStyle 	 
LeftButton;   5MinerLampMangement.view.viewbean.PasswordBoxWaterMark ��Name ��Name
 � Tag 
 CloseButton ��Name �� ImageSource  
MyCheckBox�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ;viewbean.clr-namespace:MinerLampMangement.view.viewbean  %�
  &	        &	 Z     &	 o     &	 �     &	 �     &	 3    7   7  7  &	 �    7   '  �    &	 �    7   &	 A    7   &		 �    'G�     &	
 h    &	 �    �� $  0.5,1&�$ 0.5,0&�.��� $ 	#FF377FED��$ 0Q�� $ 	#FF074CC0��$ 1Q��� $ 	#FF2D78F4���� $ 	#FF317EF3���� " ��.���  � ��$ Center=���  � ��$ 23����  � ��$ 23����  � ��$ 3,3,3,3q��� " ��.���  � ��$ Center=���  � ��$ Center=��� " ��.���  � ��$ 25����  � ��$ 65����  � ��8   ��  � ��
 �������  � ��8  ��  � �� �� " ��.R��� $	 border��#��z��#��z��#��z��
 . $ 3,3,3,3��._��� $ contentPresenter��|�.  #��z��#��z����. #
 z
 #��z�� P�  �@J�IsMouseOver . . ��� $ border��8   � ��P�  � �� . . ���  � z� �� $ .9Q�$ .9Q���  �@J�RenderTransformOrigin$ .5,.5&��� "   .���  �@  AutoGenerateColumns .  ��  �@  CanUserAddRows .  ��  �@  
IsReadOnly . ��  � ��8   �� "  .���  � ��
 ��  ���  � ��$ 25����  � ��
 �   � P�  �@ AlternationIndex$ 0��. ���  � �� �#e7e7e7P�  �@ AlternationIndex$ 1��. ���  � ��
 �����P�  �@J�IsMouseOver . . ���  � ��
 �����P�  �@ 
IsSelected . . ���  � ��
 �   ��� "  .���  � ��$ Center=���  � ��8   ��  � ��
 �������  � ��$ 13(��� " 9�.���  � ��
 �������  � ��$ 12(���  � ��8   ��  � ��
 �������  � ��$ 3q��� " ��.���  � ��
 �������  � ��$ 0q���  � ��$ 25���� " G�.���  �@ IsMonitoring . ��  � �� �� " G�.R��� $	 border��#��z��#��z��#��z��
 . ._�� .+��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=��� $ 	WaterMark��|�.  $y�	Collapsed=�#��z #��z��
������� P�  �@ PasswordLength$ 0��. ��� $ 	WaterMark�� � y�$ Visible=��� " ��.���  � �� �� " ��.R�� .+��� $ 	rectangle������ $ ../Image/close_normal.png�� P�  �@J�	IsFocused . P�  �@��IsDefaulted . P�  �@J�IsMouseOver . . ��� $ 	rectangle�� � �� �� $ ../Image/close_hover.png��P�  � �� . . ��� $ 	rectangle�� � �� �� $  ../Image/close_pressed.png��P�  � {� .  �� " ��.���  �@�OverridesDefaultStyle .  P�  �@d�	IsChecked$
 TrueY�. ���  �@�ToolTip$ 开启柜组巡检功能����  � �� �� " ��.R��� $��../Image/star.png��P�  �@d�	IsChecked$ FalseY�. ���  �@�ToolTip$ 关闭柜组巡检功能����  � �� �� " ��.R��� $��../Image/stop.png��