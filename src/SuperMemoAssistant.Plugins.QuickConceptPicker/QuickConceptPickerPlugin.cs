#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   6/7/2020 2:09:22 AM
// Modified By:  james

#endregion




namespace SuperMemoAssistant.Plugins.QuickConceptPicker
{
    using System.Diagnostics.CodeAnalysis;
  using System.Linq;
  using System.Windows;
  using System.Windows.Input;
  using SuperMemoAssistant.Extensions;
  using SuperMemoAssistant.Plugins.QuickConceptPicker.UI;
  using SuperMemoAssistant.Services;
  using SuperMemoAssistant.Services.IO.Keyboard;
  using SuperMemoAssistant.Services.Sentry;
  using SuperMemoAssistant.Sys.IO.Devices;

  // ReSharper disable once UnusedMember.Global
  // ReSharper disable once ClassNeverInstantiated.Global
  [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
  public class QuickConceptPickerPlugin : SentrySMAPluginBase<QuickConceptPickerPlugin>
  {
    #region Constructors

    /// <inheritdoc />
    public QuickConceptPickerPlugin() : base("Enter your Sentry.io api key (strongly recommended)") { }

    #endregion




    #region Properties Impl - Public

    /// <inheritdoc />
    public override string Name => "QuickConceptPicker";

    /// <inheritdoc />
    public override bool HasSettings => false;

    #endregion




    #region Methods Impl

    /// <inheritdoc />
    protected override void PluginInit()
    {
      Svc.HotKeyManager
        .RegisterGlobal(
        "SelectConcept",
        "Open Concept Selection Window",
        HotKeyScopes.SMBrowser,
        new HotKey(Key.O, KeyModifiers.Alt),
        OpenConceptSelectionWdw
      );
    }
    private void OpenConceptSelectionWdw()
    {
      var concepts = Svc.SM.Registry.Concept.ToList();

      Application.Current.Dispatcher.Invoke(() =>
      {
        var wdw = new ConceptPickerWdw(concepts);
        wdw.ShowAndActivate();
      });
    }

    #endregion


    #region Methods

    #endregion
  }
}
