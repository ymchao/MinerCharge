   M S B A M L   `   `   `  ����M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:MinerLampMangement.view.viewbean MinerLampMangement.view.viewbean  5   	   ,    &MinerLampMangement.view.ChangePassword   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:MinerLampMangement.view.viewbean     5   	     � Title$  修改密码��5   	   $	��300��6   $	��500��6#    � WindowStyle$
 None=�6/    � WindowStartupLocation$ CenterScreen=�6B   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��../Image/logo2.png��6   $
��Fill=�6,   $��5��6;   $��6��6O   �� 5   
   $��用户名：��6   $��1��6    $��1��6-      TextBlockstyle#��[  6=   $
��Bold"�6e   �� 5   
   $��旧密码：��6   $��2��6    $��1��6-   #��[  6=   $
��Bold"�6e   �� 5   
   $��新密码：��6   $��3��6    $��1��6-   #��[  6=   $
��Bold"�6e   �� 5   
   $��确认密码：��6   $��4��6!   $��1��6.   #��[  6>   $
��Bold"�6f   �� 5   
   -   5         ��Name$ Usernametxb��5      $��1��6'   $��2��64   $��2��6D     Textboxstyle#��[ 5      �� 5    
   -   5        $ Oldpasswordtxb��5       $��2��6*   $��2��67   $��2��6G   #��[ 5!      G� 5"   
   -   5         G�Name$ PasswordBox1��5"      $��3��6,   $��2��69   $��2��6I   $��3,12.9,3,12.9q�6]   $��Center=�5#      G� 5$   
   -   5        $ PasswordBox2��5$      $��4��6,   $��2��69   $��2��6I   $��3,12.9,3,12.9q�6]   $��Center=�5%      �� 5&   
   -   5         ��Name$ ChangeButton��5&      $��修改��6D   $��5��6Q   $��3��6^     MyWpfButton#��[ 5'      �� 5(   
   -   5        $ CloseButton��5(      $��退出��6B   $��5��6O   $��1��6\   #��[ 5)      5*      5+      