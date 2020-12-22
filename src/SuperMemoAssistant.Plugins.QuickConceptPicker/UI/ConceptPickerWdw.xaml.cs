using SuperMemoAssistant.Interop.SuperMemo.Registry.Members;
using SuperMemoAssistant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SuperMemoAssistant.Plugins.QuickConceptPicker.UI
{
  /// <summary>
  /// Interaction logic for ConceptPickerWdw.xaml
  /// </summary>
  public partial class ConceptPickerWdw : Window
  {
    public ConceptPickerWdw(List<IConcept> Concepts)
    {
      if (Concepts == null || Concepts.Count == 0)
      {
        return;
      }

      InitializeComponent();
      DataContext = this;

      // TODO: Fails, I think due to accessing the Name field
      Dictionary<string, IConcept> conceptMap = new Dictionary<string, IConcept>();
      foreach (var concept in Concepts)
      {
        if (!string.IsNullOrEmpty(concept.Name))
        {
          conceptMap.Add(concept.Name, concept);
        }
      }

      ConceptBox.DisplayMemberPath = "Key";
      ConceptBox.SelectedValuePath = "Value";

      ConceptBox.ItemsSource = conceptMap;

      ConceptBox.Dispatcher.BeginInvoke((Action)(() =>
      {
        ConceptBox.Focus();
      }), DispatcherPriority.Render);
    }

    private void ConceptBox_TextInput(object sender, TextChangedEventArgs e)
    {
      SelectedConceptTextblock.Content = string.Empty;

      if (ConceptBox.SelectedItem != null)
      {
        var currentConcept = (IConcept)ConceptBox.SelectedValue;
        SelectedConceptTextblock.Content = currentConcept.Name;
      }
    }

    private void BtnCancel_Click(object sender,
                             RoutedEventArgs e)
    {
      Close();
    }

    private void SetCurrentConcept()
    {
      if (ConceptBox.SelectedItem != null)
      {
        IConcept currentConcept = (IConcept)ConceptBox.SelectedValue;
        Svc.SM.UI.ElementWdw.SetCurrentConcept(currentConcept.Id);
      }
    }

    private void ReturnToElement(int elementId)
    {
      if (elementId < 0)
      {
        return;
      }
      Svc.SM.UI.ElementWdw.GoToElement(elementId);
    }
    private void BtnOk_Click(object sender,
                         RoutedEventArgs e)
    {

      int returnElementId = Svc.SM.UI.ElementWdw.CurrentElementId;
      SetCurrentConcept();
      ReturnToElement(returnElementId);

      Close();
    }

    private void ConceptBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        BtnOk_Click(sender, null);
      }
    }
  }
}
