   M S B A M L   `   `   `  ����   矿灯管理平台'    !MinerLampMangement.view.InOutData   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  数据库处理��$	��300��$	��700�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� .@��� $$��员工信息数据导入导出��.���� $��导入数据��.���  � RowDefinitions �� $��30���� $��40���� $��40���� $��40���� $��40����  � ColumnDefinitions �� �� $��2* ��� $��50 �.+��� $��1��$0��*选择要导入的员工信息CSV文件：��$��Center=� �� TextAlignment$ Right=��� -    ��Name$ OpenMinerInfoExeclBtn��$��2��$��打开��   MyWpfButton#��[  $��Right=��� -    ��Name$ ExcelNameBox��$��2��$��1��$��30�� �� 
IsReadOnly .   Textboxstyle#��[ �� $��3��$'��!导出员工信息到CSV文件：��$��Center=�$ Right=��� -   $ ExportMinerInfoBtn��$��4��$��导出��#��[  $��Right=��� $��4��$��1��$��30�� . #��[ �� $��TM卡数据导入导出��.���� $��导入数据��.���  �� $��30���� $��40���� $��40���� $��40���� $��40����  �� �� $��2* ��� $��50 �.+��� $��1��$,��&选择要导入的TM卡信息CSV文件��$��Center=�$ Right=��� -   $ TmOpenExeclBtn��$��2��$��打开��#��[  $��Right=��� $��2��$��1��$��30�� . #��[ �� $��3��$&�� 导出tm卡信息到CSV文件：��$��Center=�$ Right=��� -   $ ExportTmInfoBtn��$��4��$��导出��#��[  $��Right=��� $��4��$��1��$��30�� . #��[ �� $��删除历史数据��.���� $��删除数据��.��� .+��� -   	 ��Name$	 CheckBox��$�� 矿灯充电记录��$
��Left=�$	��Top=�$��
100,73,0,0q�$��15(�$��Center=�$��Center=��� $
��Left=�$
��Wrap=�$��删除此日期前：��$	��Top=�$��
420,50,0,0q�$��15(�(  "System.Windows.Controls.DatePicker  -   
  Name$
 
DatePicker��$
��Left=�$	��Top=�$��
420,75,0,0q�$	��165��$��15(�$��Stretch=�$��Center=��� -   $ 	Deletebtn��$��删除��$
��Left=�$	��Top=�$��75��$��455,140,0,0q�#��[  