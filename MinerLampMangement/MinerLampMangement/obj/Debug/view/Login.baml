   M S B A M L   `   `   `  ����M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinerLampMangement.viewMinerLampMangement.view  5   	   #    MinerLampMangement.view.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:MinerLampMangement.view     5   	     � Title$  Login��5   	   $	��350��6   $	��500��6$    � WindowStyle$
 None=�60    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $��30 �6   �� 5      �� 5      �� 5      �� 5      �� 5      $��30 �6   .+�5   
   �� $��6��6   $��../Image/logo2.png��6$   $
��Fill=�6@   �� 5   
   $��2��6   $��1��6   $��2��6-   $��../Image/logo3.jpg��6>   �� 5   
   -   5         ��Name$ Usernamebox��5      $��2��6'   $��2��64   $��2��6D   $��2,2,2,2q�6X   $��14(�5      $��Center=�6    .��6B    � 	Resources 5         HintText j� Stretch	 j� 
AlignmentX
 !� Visual��%�   &	        !� 5      $
 None=�6/   $
	 Left=�6>   
 5       �� 5!      $��请输入用户名��6$   $��14(�62   
�������6@   5#      5$      .��6!   ��5%      �� 5&       �� 
TargetType" ��6    �� Triggers 5'      P� 5(       P� Property � �� P� Value 62   Z� . �5)      ��  �� Property � �� �� Value# [  6;   5*      P� 5+       � ��$  ��62   . �5,      ��  � ��# [  6;   5-      5/      .��50      51      G� 52   
   -   5         G�Name$ Passwordbox��52      $��3��6+   $��2��68   $��2��6H   $��2,0,2,2q�6\   $��14(�53      $��Center=�6$   #��[BG�6F   
 � Tag$  密码��6t   �� 54   
   -   5         �� 	IsDefault . $��2��54   :   $��4��6G   $��2��6W   $��50��6h     MyWpfButton#��[ 6t   .��6�   登陆6�   �� 55   
   -   5        $��25��55      $��25��6     CloseButton#��[ 6D   $��5��6i   $��5.4,0,0,169.4q�6y   �� 56   
   -   5        $��注册账号��56      ���	#FF377FED$��4��67   $��2��6D    �� TextAlignment$ Right=�6T   $��0,10,2,0q�6j   6|   �� 57   
   -   5        $��修改密码��57      ���	#FF377FED$��4��67   $��3��6D   $ Right=�6T   $��0,10,2,0q�6j   6|   59      5:      