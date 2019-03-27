﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using NUnit.Framework;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;

namespace osu.Framework.Tests.Visual.Sprites
{
    [TestFixture]
    public class TestCaseSpriteIcon : TestCase
    {
        public TestCaseSpriteIcon()
        {
            FillFlowContainer<Icon> flow;

            Add(new TooltipContainer
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = Color4.Teal,
                        RelativeSizeAxes = Axes.Both,
                    },
                    new ScrollContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = flow = new FillFlowContainer<Icon>
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Full,
                        },
                    }
                }
            });

            foreach (FontAwesome fa in Enum.GetValues(typeof(FontAwesome)))
                flow.Add(new Icon(fa));

            AddStep("toggle shadows", () => flow.Children.ForEach(i => i.SpriteIcon.Shadow = !i.SpriteIcon.Shadow));
            AddStep("change icons", () => flow.Children.ForEach(i => i.SpriteIcon.Icon = i.SpriteIcon.Icon + 1));
        }

        private class Icon : Container, IHasTooltip
        {
            public string TooltipText { get; }

            public SpriteIcon SpriteIcon { get; }

            public Icon(FontAwesome fa)
            {
                TooltipText = fa.ToString();

                AutoSizeAxes = Axes.Both;
                Child = SpriteIcon = new SpriteIcon
                {
                    Icon = fa,
                    Size = new Vector2(60),
                };
            }
        }
    }
}
