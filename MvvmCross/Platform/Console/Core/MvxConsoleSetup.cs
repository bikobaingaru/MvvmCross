﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using MvvmCross.Plugins;
using MvvmCross.Core;
using MvvmCross.Platform.Console.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace MvvmCross.Platform.Console.Core
{
    public abstract class MvxConsoleSetup
        : MvxSetup
    {
        protected override IMvxNameMapping CreateViewToViewModelNaming()
        {
            return new MvxPostfixAwareViewToViewModelNameMapping("View");
        }

        public virtual void InitializeMessagePump()
        {
            var messagePump = new MvxConsoleMessagePump();
            Mvx.RegisterSingleton<IMvxMessagePump>(messagePump);
            Mvx.RegisterSingleton<IMvxConsoleCurrentView>(messagePump);
        }

        protected override IMvxViewsContainer CreateViewsContainer()
        {
            var container = CreateConsoleContainer();
            Mvx.RegisterSingleton<IMvxConsoleNavigation>(container);
            return container;
        }

        protected override IMvxViewDispatcher CreateViewDispatcher()
        {
            return new MvxConsoleViewDispatcher();
        }

        protected virtual MvxBaseConsoleContainer CreateConsoleContainer()
        {
            return new MvxConsoleContainer();
        }

        protected override void InitializeLastChance()
        {
            InitializeMessagePump();
        }

        protected override IMvxPluginManager CreatePluginManager()
        {
            // Console is 'full .net' - so uses the same plugins as Wpf
            return new MvxFilePluginManager(".Wpf", string.Empty);
        }
    }
}
