// <copyright file="StyleSheetRegistrar.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Style
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Reflection;
	using Xamarin.Forms.StyleSheets;

	/// <summary>Style sheet registrat.</summary>
	public static class StyleSheetRegistrar
	{
		/// <summary>Register style sheet.</summary>
		/// <param name="name">Name of style sheet.</param>
		/// <param name="targetType">Instance target type.</param>
		/// <param name="bindablePropertyName">Bound property name.</param>
		public static void RegisterStyle(string name, Type targetType, string bindablePropertyName)
		{
			PropertyInfo stylePropertyInfo = typeof(Xamarin.Forms.Internals.Registrar).GetProperty("StyleProperties", BindingFlags.Static | BindingFlags.NonPublic);
			if (stylePropertyInfo == null)
			{
				return;
			}

			object styleProperties = stylePropertyInfo.GetValue(null);

			Type styleAttributeType = typeof(StyleSheet).Assembly.GetType("Xamarin.Forms.StyleSheets.StylePropertyAttribute");
			object styleAttributeInstance = Activator.CreateInstance(styleAttributeType, name, targetType, bindablePropertyName);

			MethodInfo dictionaryAdd = styleProperties.GetType().GetMethod("Add");
			if (dictionaryAdd == null)
			{
				return;
			}

			Type styleListType = typeof(List<>).MakeGenericType(styleAttributeType);
			IList styleList = (IList)Activator.CreateInstance(styleListType);

			styleList.Add(styleAttributeInstance);
			dictionaryAdd.Invoke(styleProperties, new object[] { name, styleList });
		}
	}
}