   M S B A M L   `   `   `  ����   矿灯管理平台4    .MinerLampMangement.view.view2.MinerDoorControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � 	Resources   
  Simple �� 
TargetType �� Property �� Value��%Z   &	        �� " _�.���  �@_�VerticalOffset$ 20����  � �� �	#FF65C3B1 � ToolTip _� #��[  
�������.���  � RowDefinitions �� ��  � ColumnDefinitions �� �� .+��� $��0��$��0��$ ��../../Image/clien-list.png��$��Right=�$��Center=��� $��职工信息��$��0��$��1��$
��Bold"�$��15(�$
��Left=�$��Center=��� -    ��Name$ 
StackPanel��$��1��$��2��.+��� -    ��Name$ 	minername��$��	姓名：���� -   $ minernum��$��	工号：���� -   $ 	chargenum��$��充电次数：���� -   $ 	lampstyle��$��矿灯型号：���� -   $ status��$��	状态：���� -   $ 
statustime��$��状态存在：���� -	   $ 
department��$��	部门：��.��� -
   	 �Name$	 GridMain�� �� $��2* ��� .+��� -   
 ��Name$
 Statusimage��$��0��$��/Image/DelPeople.ico��$	��0,3q�(� $��1��.���� -   $ minernameshow��$��信息待获取...��$	��0,3q�