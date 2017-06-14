﻿using System.Collections.Generic;

namespace MarkdownDeep
{
	public static class Extensions
	{
		public static List<Heading> ConvertToHierarchy(this List<Heading> headings)
		{
			var hierarchy = new List<Heading>();

			for (var i = 0; i < headings.Count; i++)
			{
				if (i > 0)
				{
					var previousHeading = headings[i - 1];
					var currentHeading = headings[i];

					SetParentForHeading(previousHeading, currentHeading);

					var parent = currentHeading.Parent;
					if (parent == null)
					{
						hierarchy.Add(currentHeading);
					}
					else
					{
						parent.Children.Add(currentHeading);
					}
				}
				else
				{
					hierarchy.Add(headings[i]);
				}
			}

			return hierarchy;
		}

		private static void SetParentForHeading(Heading previousHeading, Heading headingToAdd)
		{
			if (previousHeading.Level == headingToAdd.Level)
			{
				headingToAdd.Parent = previousHeading.Parent;
			}
			else if (previousHeading.Level < headingToAdd.Level)
			{
				headingToAdd.Parent = previousHeading;
			}
			else if (previousHeading.Level > headingToAdd.Level)
			{
				var previousHeadingParent = previousHeading.Parent;
				if (previousHeadingParent != null)
				{
					SetParentForHeading(previousHeadingParent, headingToAdd);
				}
			}
		}
	}
}