   M S B A M L   `   `   `  ���� E  ATourAgency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TourAgency
TourAgency      TourAgency.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:TourAgency     c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   $	��700��$	��900��   ModifiedWindow#���     � WindowStartupLocation$  CenterScreen=�.����  �� CornerRadius$ 30 10 10 10��  	LightGray#���  ._��  � RowDefinitions �� $��50 ��� $
��Auto �.+�� -   
������ .+�� $
��Left=�$	��250�� �� $��25 ��� .+��� $��1��  SeaGreen#���  $	��100��$
��Left=��� $
��Left=�$	��250��$��50��$��2��$ 	30 0 0 30��#���  ._��� $��Дивний світ�� �� Padding$
 0 14q�$��Center=�$��20(�$��Black"�#���  ��  �� Orientation$ 
Horizontal=�$��Right=�$	��Top=�$��10 10q�.+��� -    ��Name$ 
userButton��$��Увійти��$
��Bold"�$��	0 0 120 0q�
 � Tag$ Увійти��  StrokeButton#���  � $��1�� � ColumnDefinitions �� $	��250 ��� .+��� #���  $	��650��$ 	0 30 0 10��._�� $��0 20 0 0q� �� $��40 ��� $��* �.+�� $��0��$	��Top=�.+��� -    ��Name$ 	searchBox��$��	20 0 20 0q�$ 
Пошук��  AgencyTextBox#���  �� -   	 ��Name$	 tourList��$��Right=�$��1��$	��Top=�$��0q�$��False��
������ $��0q�$��0q�  TourListView#���    TourListViewItem#���  �� -   
 ��Name$
 	infoPanel��$��1��$��10q�$y�	Collapsed=�.+�   TourAgency.ClippingBorder  $��90������  �� 
TargetType" ��.���  �� Property �@��CornerRadius �� Value$ 20����  � ��  AlmostWhite# �     Child. � .+��� -    ��Name$ tourName��$��Bottom=�$
��Left=�$��25q�#���  $��36(�$
��Bold"��� $��Bottom=�$��Right=�$ 
Horizontal=�.+��� -   $ 	tourPrice�����#FACB4B$
��Bold"�$��18(�$��Center=�$��Center=��� -   $ orderButton��  GreenControl#���  $��Замовити��$	��150��$��60��$��15q�${�False��$��16(��� $��	20 10 0 0q�.+��� .+��� $��КРАЇНА��$��11(�$��0 10 5 0q� 	 Gray#��� 	 �� -	   $ tourCountry��$��16(�$��0 0 5 10q��� .+��� $#��ТРИВАЛІСТЬ ТУРУ��$��11(�$��0 10 5 0q�#��� 	 �� -
   $ tourDuration��$��16(�$��0 0 5 10q��� .+��� $'��!ДАТА ВІДПРАВЛЕННЯ��$��11(�$��0 10 5 0q�#��� 	 �� -   $ tourDate��$��16(�$��0 0 5 10q��� .+��� $'��!КІЛЬКІСТЬ КВИТКІВ��$��11(�$��0 10 5 0q�#��� 	 �� $ 
Horizontal=�.+��� -   $ tourCurrentTicketNumber��$��16(�$��0 0 0 10q��� $��12(�$	�� / ��$��0 0 0 10q�$��Bottom=��� -   $ tourMaxTicketNumber��$��12(�$��0 0 0 10q�$��Bottom=��� .+��� $��ОПИС��$��11(�$��0 10 5 0q�#��� 	 �� -   $ tourDescription��$��16(�$
��Wrap=�$��0 0 5 10q�  -     Name$ confirmOrder��$��1��#���  $ 10��$��Center=�$��Center=�$y�	Collapsed=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$ .5Q�   ShadowDepth$ 0Q�   Color$ #212121��. �  �� $��50 ��� .+�� .+��� $ ��Підтвердження��$
 0 14q�$��Center=�$��18(�$��Black"�#���  �� -   $��X�� 
 MainControl#��� 
   SubCloseButton#���  $��Right=�$	��Top=�$��50���� $��1��#���  $��20q�$ 10��$	��310��$	��250��._��� $��15q�.+��� $��0 0 0 10q�.+��� $��ВАШЕ ІМ'Я��$��10(�#���  $	��0 5q��� -   $ userNameBox��$
��name��${�False��#���  $  ���� $��0 0 0 10q�.+��� $.��(ВАША ЕЛЕКТРОННА ПОШТА��$��10(�#���  $	��0 5q��� -   $ userEmailBox��$��email��${�False��#���  $  ���� $��0 0 0 10q�.+��� $'��!КІЛЬКІСТЬ КВИТКІВ��$��10(�#���  $	��0 5q��� -   $ ticketsCountBox��$��1��#���  $  ���� $ 
Horizontal=�$��0 20 0 0q�.+��� $��
ЦІНА: ��#���  $
��Bold"�$��18(��� -   $ 
priceBlock��$��0��#���  $
��Bold"�$��18(��� $
�� ₴��#���  $
��Bold"�$��18(��� -     Control#���  $��Замовити��$��40��$��0 5 0 0q��� -    ��Name$ 	loginForm��$��2��$y�Visible=�$ 25 10 10 10������  �� Color$ #212121�� �� Opacity$ .5Q�._�  $	��400��$	��250��$ 10��#���        
BlurRadius$ 20Q�$ 0Q�$ .3Q�. �  �� $��50 ��� $
��Auto �.+�� .+��� $��Авторизація��$
 0 14q�$��Center=�$��18(�$��Black"�#���  �� -   $��X��#��� 
 #���  $��Right=�$	��Top=�$��50���� $��1��$��20 10q�$��Bottom=�$ 10�� �� Padding$ 10q�#���  ._��� .+��� -   $ logEmailBox��$��
10 10 10 5q�#���  $% Електронна пошта���� -   $ 
logPassBox��$��	10 5 10 5q�#���  $ Пароль���� -   $��Увійти��#���  $��30��$��10q��� -     
TextButton#���  $% Не маєте акаунта?���� -   $ regForm��$��2��$ 25 10 10 10��$y�	Collapsed=����� $ #212121��$ .5Q�._�  $ 10��$	��400��$	��370��#���     $ 20Q�$ 0Q�$ .3Q�. �  �� $��50 ��� $
��Auto �.+�� 
������ .+��� $��Реєстрація��$
 0 14q�$��Center=�$��18(�$��Black"�#���  �� -   $��X��#��� 
 #���  $��Right=�$	��Top=��� $��1��$��20 10q�$��Bottom=�$ 10��$ 10q�#���  ._��� .+��� -   $ regSurnameBox��$��
10 10 10 5q�#���  $) #Прізвище (латинкою)���� -   $ 
regNameBox��$��	10 5 10 5q�#���  $  Ім'я (латинкою)���� -    $ regEmailBox��$��	10 5 10 5q�#���  $% Електронна пошта���� -!   $ 
regPassBox��$��	10 5 10 5q�#���  $ Пароль���� -"   $ regRepeatPassBox��$��	10 5 10 5q�#���  $% Повторіть пароль���� -#   $$��Зареєструватися��#���  $��30��$��10q��� -$   #���  $% Вже маєте акаунт?��  -%   $ userOrdersPanel��$y�	Collapsed=�$��1��$ 10��$	��800��$	��600��#���     $ 5Q�$ 0Q�$ .5Q�. �� .+�� .+��� $!��Мої бронювання��$��16(�$
��Bold"�#���  $��Center=�$��Center=��� -&   $��Right=�$��X��#��� 
 #���  �� .+�&   System.Windows.Controls.DataGrid  -'     Name$ 
userOrders��$��10q�  ModifiedDataGrid#���    ModifiedGridCell   	CellStyle# �    ModifiedGridRow   RowStyle# �    ModifiedGridColumnHeader   ColumnHeaderStyle# �     Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 15     Header$  C��!   CellTemplate! �� .R��� $ 5��$��10��$��30��$��Center=� �� *Status+  EnumToStringConverter" �� 	Converter#" [ ���� " ��.���  � ��# �  # �� Triggers# P� $ P� Property$ �@�Tag% P� Value$% Actual��. ���  � ��
 ��iA�P� $ �@�Tag$% Current��. ���  � ��
 �/���P� $ �@�Tag$% Happened��. ���  � ��
 �\\��P� $ �@�Tag$% Canceled��. ���  � ��
 �����0  *System.Windows.Controls.DataGridTextColumn  $  #��$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& �� *Id+  $  Користувач��$ * & �� *
UserName+  $  Тур��$ * & �� *City+  $  Вартість, грн��$ * & �� *Price+  $  Квитки, шт��$ * & �� *TicketNumber+  $'  !Дата відправлення��$
 1.6* & �� *Date+' �� StringFormat$' 
dd.MM.yyyy��  $  Скасування��$
 Auto ! �� .R��� ._��� -(   $��2��$��50��$��50�� �� *Status+���� " ��.���  � ��
 �\\����  � �� �� ( �� 
TargetType"( ��.R��� #��z��$ 25��._�.	  (MahApps.Metro.IconPacks.PackIconMaterial	  ) 	  Kind$) Cancel=�$��Center=�$��Center=�* �� Triggers* P� $ � ��$
% True��. ���  � ��
 � E���� -)   $ ordersAreEmpty��$#��Бронювань немає��$��Center=����#555555�� -*   $	 	userPanel��$y�	Collapsed=�$��2��$	��100��$	��170��$	��Top=�$��Right=�$��120 50q�  UserListView#���    UserItemContainer#���  .@��� -+   + ��Name$+ userOrdersButton��$! Мої бронювання���� -,   $+ adminToolButton��$/ )Панель адміністратора���� --   $+ logOutButton��$ 
Вихід���� $ 
Horizontal=�$��Right=�$
��10 0q�.+��� -.   $	��—��#��� 
   	MinButton#���  �� -/   $��X��#��� 
   CloseButton#���  