   M S B A M L   `   `   `  ����M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinerLampMangement.viewMinerLampMangement.view  5   	   $    MinerLampMangement.view.SignUp   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:MinerLampMangement.view     5   	     � Title$  账号注册��5   	   $	��300��6   $	��500��6#    � WindowStyle$
 None=�6/    � WindowStartupLocation$ CenterScreen=�6B   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��../Image/logo2.png��6   $
��Fill=�6,   $��5��6;   $��5��6O   6b   �� 5      $��用户名：��6   $��1��6"   $��1��6/      TextBlockstyle#��[  6?   $
��Bold"�6g   6{   �� 5   
   $��	密码：��6   $��2��6   $��1��6,   #��[  6<   $
��Bold"�6d   6x   �� 5   
   $��确认密码：��6   $��3��6!   $��1��6.   #��[  6>   $
��Bold"�6f   6z   �� 5   
   -   5         ��Name$ Usernametxb��5       � ToolTip$0 *以字母和数字的组合作为用户名��6'   $��1��6@   $��2��6M   $��2��6]     Textboxstyle#��[ 6q   6�   G� 5   
   -   5         G�Name$ PasswordBox1��5      $- '以字母和数字的组合作为密码��6,   $��2��6D   $��2��6Q   $��2��6a   $��3,18.1,3,18.1q�6u   $��Center=�6�   6�   G� 5   
   -   5        $ PasswordBox2��5      $- '以字母和数字的组合作为密码��6,   $��3��6D   $��2��6Q   $��2��6a   $��3,18.1,3,18.1q�6u   $��Center=�6�   6�   �� 5   
   -   5         ��Name$ SignUpButton��5      $��注册��6D   $��4��6Q   $��3��6^     MyWpfButton#��[ 6n   6�   �� 5    
   -   5        $ CloseButton��5       $��退出��6B   $��4��6O   $��1��6\   #��[ 6l   6�   5"      5#      