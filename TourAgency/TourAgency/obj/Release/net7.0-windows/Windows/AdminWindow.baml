   M S B A M L   `   `   `  ���� E  ATourAgency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TourAgency
TourAgency      TourAgency.AdminWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   &localclr-namespace:TourAgency   $	��800��$
��1100��   ModifiedWindow#���     � WindowStartupLocation$  CenterScreen=�.����   	LightGray#���   �� CornerRadius$ 10��._��  � RowDefinitions �� $��50 ��� .+�� -   
������ .+��� $��
Admin Tool�� �� Padding$
 0 14q�$��Center=�$��18(�$��Black"�  SeaGreen#���  ��  �� Orientation$ 
Horizontal=�$��Right=�$	��Top=�$��10 10q�.+��� -   $	��—��  MainControl#���    	MinButton#���  �� -   $��X��#���    CloseButton#���  � $��1�� � ColumnDefinitions �� $
��Auto ��� .+��� $
��Left=�$	��Top=�.+��� #���  $��20 10q�$ 20��._��� $��20 10q�$	��250��#���  .+��� $��Новий тур��$��Center=�#���  $��Black"�$��20(�$��0 0 0 5q��� -    ��Name$ 
countryBox��  AgencyTextBox#���  
 � Tag$ Країна��$��0 10 0 0q��� -   $ nameBox��#���  $ 
Місто��$��0 10 0 0q��� -   $ dateBox��#���  $! Дата відправки��$��0 10 0 0q��� $ 
Horizontal=�$��0 10 0 0q�.+��� -   $ durationBox��#���  $	��210��$# Тривалість туру���� $��
  днів��$��Center=�$��Right=�$
��Bold"�#���  �� $ 
Horizontal=�$��0 10 0 0q�.+��� -   $ priceBox��#���  $	��210��$ Вартість���� $��  грн��$��Center=�$��Right=�$
��Bold"�#���  �� -	   $ ticketCountBox��#���  $' !Кількість квитків��$��0 10 0 0q��� -
   $ descriptionBox�� �� TextWrapping$
 Wrap=�$��5q�	 �� AcceptsReturn$
	 True��$	��150��$	��Top=�#���  $ Опис��$	��Top=�$��0 10 0 0q��� -   $��Зберегти��$��	0 20 0 10q�  Control#���  $	��250��$��40���� $��1��$��0 10 0 0q�$
��Left=�._��  �� $	��240 ��� .+�   TourAgency.ClippingBorder  
 �� Padding$
 0 0 20 0q�   Child. �� .+�&   System.Windows.Controls.DataGrid  -     Name$ 	orderList��$	��240��$��10q�   SelectionMode$ Single=�  ModifiedDataGrid#���   	 ModifiedGridCell   	CellStyle# � 	  
 ModifiedGridRow   RowStyle# � 
   ModifiedGridColumnHeader   ColumnHeaderStyle# �     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ #��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Користувач��$
 1.2*  �� *
UserName+  $ Тур��$
 1.2*  �� *City+  $ Вартість, грн��$ *  �� *Price+  $ Квитки, шт�� �� *TicketNumber+  $' !Дата відправлення��$
 1.6*  �� *Date+ �� StringFormat$ 
dd.MM.yyyy��  $ Стан��$ *  �� *Status+�� -    ��Name$ ordersAreEmpty��$#��Бронювань немає��$��Center=����#555555  $
 0 0 20 0q�$��1��. �� .+�  -   $ tourList��$	��345��$��10q�$ Single=�#���  # � 	 # � 
 # �     $ #��$
 Auto  �� *Id+  $ 
Місто��$ *  �� *City+  $ Країна��$ *  �� *	Country+  $' !Дата відправлення��$
 1.2*  �� *Date+$ 
dd.MM.yyyy��  $ Ціна, грн��$ *  �� *Price+4  .System.Windows.Controls.DataGridTemplateColumn  $ Операції��$
 Auto    CellTemplate �� .R��� $ 
Horizontal=�.+�  $ 20��. �� -   $��0 0 1 0q�
����d�  OperationControl#���   �� *Status+  EnumToStringConverter �� 	Converter# [ .��.  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ PencilOutline=�  $ 20��. �� -   $��1 0 0 0q�
���\\��#���   �� *Status+# [ .��  $ TrashCanOutline=��� -   $ toursAreEmpty��$��Турів немає��$��Center=�  DarkGray#���    -     Name$ editCurrentTour��$ 10��$y�	Collapsed=�$��2��  AlmostWhite#���  $��Center=�$��Center=� J� Effect 3	  -System.Windows.Media.Effects.DropShadowEffect	   	  
BlurRadius$ 20Q� 	  ShadowDepth$ 0Q� 	  Opacity$ .3Q�. �  �� $��50 ��� .+�� .+��� $��Редагування��$
 0 14q�$��Center=�$��18(�$��Black"�#���  �� -   $��X��#���    SubCloseButton#���  $��Right=�$	��Top=�$��50���� $��1��#���  $��20q�$ 10��._��� $��20 10q�$	��350��#���  .+��� -   $ editCountryBox��#���  $
��0 10q�$ Країна���� -   $ editNameBox��#���  $
��0 10q�$ 
Місто���� -   $ editDateBox��#���  $
��0 10q�$! Дата відправки���� $ 
Horizontal=�$
��0 10q�.+��� -   $ editDurationBox��#���  $# Тривалість туру��$	��250���� $��
  днів��$��Center=�$��Right=�$
��Bold"�#���  �� $ 
Horizontal=�$
��0 10q�.+��� -   $ editPriceBox��#���  $ Вартість��$	��250���� $��  грн��$��Center=�$��Right=�$
��Bold"�#���  �� -   $ editTicketCountBox��#���  $
��0 10q�$' !Кількість квитків���� -   $ editDescriptionBox��$
 Wrap=�$��5q�$
	 True��$	��120��$	��Top=�$	��Top=�#���  $
��0 10q�$ Опис���� -   $��Зберегти��$��	0 20 0 10q�#���  $��40��