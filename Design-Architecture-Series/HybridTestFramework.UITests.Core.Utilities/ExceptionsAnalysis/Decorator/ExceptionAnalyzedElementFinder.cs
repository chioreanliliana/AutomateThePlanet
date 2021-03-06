﻿// <copyright file="ExceptionAnalyzedElementFinder.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using System;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator
{
    public class ExceptionAnalyzedElementFinder : IElementFinder
    {
        public ExceptionAnalyzedElementFinder(IElementFinder elementFinder, IUiExceptionAnalyser exceptionAnalyser)
        {
            this.ElementFinder = elementFinder;
            this.UiExceptionAnalyser = exceptionAnalyser;
        }

        public ExceptionAnalyzedElementFinder(ExceptionAnalyzedElementFinder elementFinderDecorator)
        {
            this.UiExceptionAnalyser = elementFinderDecorator.UiExceptionAnalyser;
            this.ElementFinder = elementFinderDecorator.ElementFinder;
        }

        public IUiExceptionAnalyser UiExceptionAnalyser { get; private set; }

        public IElementFinder ElementFinder { get; private set; }

        public TElement Find<TElement>(By by) where TElement : class,IElement
        {
            TElement result = default(TElement);
            try
            {
                result = this.ElementFinder.Find<TElement>(by);
            }
            catch (Exception ex)
            {
                this.UiExceptionAnalyser.Analyse(ex, this.ElementFinder);
                throw;
            }

            return result;
        }

        public IEnumerable<TElement> FindAll<TElement>(By by) where TElement : class, IElement
        {
            IEnumerable<TElement> result = default(IEnumerable<TElement>);
            try
            {
                result = this.ElementFinder.FindAll<TElement>(by);
            }
            catch (Exception ex)
            {
                this.UiExceptionAnalyser.Analyse(ex, this.ElementFinder);
                throw;
            }

            return result;
        }

        public bool IsElementPresent(By by)
        {
            bool result = default(bool);
            try
            {
                result = this.ElementFinder.IsElementPresent(by);
            }
            catch (Exception ex)
            {
                this.UiExceptionAnalyser.Analyse(ex, this.ElementFinder);
                throw;
            }

            return result;
        }
    }
}