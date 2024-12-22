using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BlazorMonaco;
using BlazorMonaco.Editor;
using Microsoft.JSInterop;

namespace SampleWebApp.Pages;

public partial class Test
{
    private const string DefaultValue = "Default editor value.";

    private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.
Curabitur eu mi sit amet lacus ultrices semper. Sed efficitur leo sapien, eget faucibus enim tempor vitae. Etiam sed tincidunt ex, at congue ipsum. Curabitur pretium nisi in nulla ornare, at viverra nisl dictum. Integer placerat a lectus in sagittis. Mauris at sapien odio. Vestibulum pharetra, sapien quis luctus pellentesque, mauris purus blandit nisl, egestas sodales eros risus in nisi. Praesent pellentesque dictum faucibus. Curabitur eu imperdiet nisi. Proin scelerisque eros vel tempor faucibus. Cras vestibulum sagittis diam, quis consequat turpis bibendum quis. Fusce iaculis lacus odio, in facilisis erat luctus et. Integer eget elit sed nulla commodo venenatis id a ante.
Donec eget dolor mattis, malesuada arcu id, blandit lacus. In hac habitasse platea dictumst. Ut lacinia tellus at elit viverra porta. Donec lorem odio, ullamcorper at ultricies vitae, placerat nec risus. Nulla eget mi sollicitudin leo auctor ultricies. Integer rhoncus libero tellus, quis laoreet quam maximus id. Etiam semper, dui sed posuere fringilla, velit ipsum accumsan mauris, at rhoncus justo purus ut felis. Nam nunc lectus, placerat suscipit augue eget, consectetur tristique diam. Vivamus id velit magna. Maecenas pulvinar risus quam, in pharetra massa aliquet vitae.
Integer sed diam justo. In efficitur semper vehicula. Ut a nibh at arcu vehicula dictum. Etiam lobortis enim ut neque interdum convallis. Mauris viverra pretium lacus, et ornare sem condimentum in. Nam tincidunt non nunc in dictum. Curabitur pretium nulla a vehicula placerat. Mauris sodales enim a enim interdum suscipit. Nulla facilisi.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse feugiat lorem magna, non scelerisque mi ultrices sed. Fusce tincidunt nunc at sodales rhoncus. Sed vehicula ligula eget purus euismod, blandit cursus nulla euismod. Sed diam orci, iaculis vitae molestie quis, malesuada vitae est. Nunc dictum finibus pretium. Fusce iaculis eleifend maximus. Donec lectus risus, tristique a turpis quis, sodales porta nulla. Fusce posuere lacus vitae purus accumsan, sit amet tempus ex tincidunt. Nam nec convallis turpis. Fusce diam turpis, consequat et congue non, posuere at neque. Sed massa mauris, consectetur sit amet volutpat non, tempus ac risus. Etiam hendrerit mauris non tincidunt fermentum. Quisque ligula dolor, tempus non diam id, bibendum scelerisque ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec risus id sem interdum tempus. Proin blandit sodales metus a porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac iaculis urna. Vestibulum suscipit blandit velit, sollicitudin consectetur libero sollicitudin sit amet. Sed suscipit metus quis massa placerat, a congue turpis volutpat. Quisque a suscipit nisl. Pellentesque eget porttitor mi. Phasellus non lorem sed lectus vestibulum malesuada.";

    private const int ValueUpdateDelayMs = 250;

    private string _testName = "all";

    private readonly Dictionary<string, Func<Task>> _tests = [];

    private readonly StandaloneEditorConstructionOptions _defaultOptions = new()
    {
        Language = "javascript",
        GlyphMargin = true,
        Value = DefaultValue,
        ScrollBeyondLastLine = false,
        RenderFinalNewline = "on",
        SelectOnLineNumbers = true
    };

    private readonly ILogger<Test> _logger;

    private readonly IJSRuntime _jsRuntime;

    [AllowNull]
    private StandaloneCodeEditor _editor;

    public Test(IJSRuntime jsRuntime, ILogger<Test> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;

        _tests = new()
        {
            // ------------------------
            // ---- Editor Methods ----
            // ------------------------

            // public Task DisposeEditor()
            // public Task<string> GetEditorType()
            // public Task Layout(Dimension dimension = null)
            // public Task Focus()
            // public Task<bool> HasTextFocus()
            // public Task<int> GetVisibleColumnFromPosition(Position position)
            // public Task<Position> GetPosition()
            // public Task SetPosition(Position position, string source)
            // public Task RevealLine(int lineNumber, ScrollType? scrollType = null)
            // public Task RevealLineInCenter(int lineNumber, ScrollType? scrollType = null)
            // public Task RevealLineInCenterIfOutsideViewport(int lineNumber, ScrollType? scrollType = null)
            // public Task RevealPosition(Position position, ScrollType? scrollType = null)
            // public Task RevealPositionInCenter(Position position, ScrollType? scrollType = null)
            // public Task RevealPositionInCenterIfOutsideViewport(Position position, ScrollType? scrollType = null)
            // public Task<Selection> GetSelection()
            // public Task<List<Selection>> GetSelections()
            // public Task SetSelection(Range selection, string source)
            // public Task SetSelection(Selection selection, string source)
            // public Task SetSelections(List<Selection> selections, string source)
            // public Task RevealLines(int startLineNumber, int endLineNumber, ScrollType? scrollType = null)
            // public Task RevealLinesInCenter(int lineNumber, int endLineNumber, ScrollType? scrollType = null)
            // public Task RevealLinesInCenterIfOutsideViewport(int lineNumber, int endLineNumber, ScrollType? scrollType = null)
            // public Task RevealRange(Range range, ScrollType? scrollType = null)
            // public Task RevealRangeInCenter(Range range, ScrollType? scrollType = null)
            // public Task RevealRangeAtTop(Range range, ScrollType? scrollType = null)
            // public Task RevealRangeInCenterIfOutsideViewport(Range range, ScrollType? scrollType = null)
            // public Task Trigger(string source, string handlerId, object payload = null)

            // --------------------------------------
            // ---- StandaloneCodeEditor Methods ----
            // --------------------------------------

            // public Task UpdateOptions(EditorUpdateOptions newOptions)
            ["AddCommand"] = AddCommand,
            ["AddAction"] = AddAction,
            // public Task ResetDeltaDecorations()

            // [Parameter] public EventCallback<ModelContentChangedEvent> OnDidChangeModelContent { get; set; }
            // [Parameter] public EventCallback<ModelLanguageChangedEvent> OnDidChangeModelLanguage { get; set; }
            // [Parameter] public EventCallback<ModelLanguageConfigurationChangedEvent> OnDidChangeModelLanguageConfiguration { get; set; }
            // [Parameter] public EventCallback<ModelOptionsChangedEvent> OnDidChangeModelOptions { get; set; }
            // [Parameter] public EventCallback<ConfigurationChangedEvent> OnDidChangeConfiguration { get; set; }
            ["OnDidChangeConfiguration"] = ChangeConfiguration,
            // [Parameter] public EventCallback<CursorPositionChangedEvent> OnDidChangeCursorPosition { get; set; }
            // [Parameter] public EventCallback<CursorSelectionChangedEvent> OnDidChangeCursorSelection { get; set; }
            // [Parameter] public EventCallback<ModelChangedEvent> OnDidChangeModel { get; set; }
            // [Parameter] public EventCallback<ModelDecorationsChangedEvent> OnDidChangeModelDecorations { get; set; }
            // [Parameter] public EventCallback OnDidFocusEditorText { get; set; }
            // [Parameter] public EventCallback OnDidBlurEditorText { get; set; }
            // [Parameter] public EventCallback OnDidFocusEditorWidget { get; set; }
            // [Parameter] public EventCallback OnDidBlurEditorWidget { get; set; }
            // [Parameter] public EventCallback OnDidCompositionStart { get; set; }
            // [Parameter] public EventCallback OnDidCompositionEnd { get; set; }
            // [Parameter] public EventCallback<PasteEvent> OnDidPaste { get; set; }
            // [Parameter] public EventCallback<EditorMouseEvent> OnMouseUp { get; set; }
            // [Parameter] public EventCallback<EditorMouseEvent> OnMouseDown { get; set; }
            // [Parameter] public EventCallback<EditorMouseEvent> OnContextMenu { get; set; }
            // [Parameter] public EventCallback<EditorMouseEvent> OnMouseMove { get; set; }
            // [Parameter] public EventCallback<PartialEditorMouseEvent> OnMouseLeave { get; set; }
            // [Parameter] public EventCallback<KeyboardEvent> OnKeyUp { get; set; }
            // [Parameter] public EventCallback<KeyboardEvent> OnKeyDown { get; set; }
            // [Parameter] public EventCallback<EditorLayoutInfo> OnDidLayoutChange { get; set; }
            // [Parameter] public EventCallback<ContentSizeChangedEvent> OnDidContentSizeChange { get; set; }
            // [Parameter] public EventCallback<ScrollEvent> OnDidScrollChange { get; set; }

            ["HasWidgetFocus"] = HasWidgetFocus,
            // public Task<TextModel> GetModel()
            // public Task SetModel(TextModel model)
            ["GetOptions"] = GetOptions,
            ["GetOption"] = GetOption,
            ["GetRawOptions"] = GetRawOptions,
            ["GetValue"] = GetValue,
            ["SetValue"] = SetValue,
            ["GetContentWidth"] = GetContentWidth,
            ["GetScrollWidth"] = GetScrollWidth,
            ["GetScrollLeft"] = GetScrollLeft,
            ["GetContentHeight"] = GetContentHeight,
            ["GetScrollHeight"] = GetScrollHeight,
            ["GetScrollTop"] = GetScrollTop,
            ["SetScrollLeft"] = SetScrollLeft,
            ["SetScrollTop"] = SetScrollTop,
            ["SetScrollPosition"] = SetScrollPosition,
            // public Task<bool> PushUndoStop()
            // public Task<bool> PopUndoStop()
            // public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, List<Selection> endCursorState)
            // public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, CursorStateComputer endCursorState)
            // public async Task<string[]> DeltaDecorations(string[] oldDecorationIds, ModelDeltaDecoration[] newDecorations)
            // public Task<EditorLayoutInfo> GetLayoutInfo()
            // public Task<List<Range>> GetVisibleRanges()
            // public Task<double> GetTopForLineNumber(int lineNumber)
            // public Task<double> GetTopForPosition(int lineNumber, int column)
            // public Task<string> GetContainerDomNodeId()
            // public Task<string> GetDomNodeId()
            // public Task<int> GetOffsetForColumn(int lineNumber, int column)
            // public Task Render(bool? forceRedraw = null)
            // public Task<BaseMouseTarget> GetTargetAtClientPoint(int clientX, int clientY)
            ["GetScrolledVisiblePosition"] = GetScrolledVisiblePosition,
        };
    }

    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return _defaultOptions;
    }

    private async Task RunTest()
    {
        foreach (var testName in _tests.Keys)
        {
            if (_testName == testName || _testName == "all")
            {
                if (_tests.TryGetValue(testName, out var test_method))
                    await test_method();
            }
        }
    }

    private async Task AddCommand()
    {
        // Arrange
        var runIsCalled = false;

        // Act
        await _editor.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.Enter, (args) =>
        {
            runIsCalled = true;
        });

        // Assert
        await _editor.Focus();
        await _jsRuntime.InvokeVoidAsync("dispatchKeydown", (int)'\r', true, false, false, false);
        AssertEquals(runIsCalled, true);
    }

    private async Task AddAction()
    {
        // Arrange
        var runIsCalled = false;
        var actionDescriptor = new ActionDescriptor
        {
            Id = "testActionId",
            Label = "testActionLabel",
            Keybindings = [(int)KeyMod.CtrlCmd | (int)KeyCode.KeyE],
            Run = (editor) =>
            {
                runIsCalled = true;
            }
        };

        // Act
        await _editor.AddAction(actionDescriptor);

        // Assert
        await _editor.Focus();
        await _jsRuntime.InvokeVoidAsync("dispatchKeydown", (int)'E', true, false, false, false);
        AssertEquals(runIsCalled, true);
    }

    private ConfigurationChangedEvent? _configurationChangedEvent;

    private async Task ChangeConfiguration()
    {
        // Arrange
        var options = new EditorUpdateOptions()
        {
            ShowFoldingControls = "always"
        };

        // Act
        await _editor.UpdateOptions(options);

        // Assert
        AssertNotNull(_configurationChangedEvent);
        AssertEquals(_configurationChangedEvent?.HasChanged(EditorOption.showFoldingControls) ?? false, true, "showFoldingControls");
        AssertEquals(_configurationChangedEvent?.HasChanged(EditorOption.showDeprecated) ?? false, false, "showDeprecated");
    }

    private void OnDidChangeConfiguration(ConfigurationChangedEvent configurationChangedEvent)
    {
        _configurationChangedEvent = configurationChangedEvent;
    }

    private async Task HasWidgetFocus()
    {
        // Arrange
        await _jsRuntime.InvokeVoidAsync("focusElement", "select");
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var focusBefore = await _editor.HasWidgetFocus();
        await _editor.Focus();
        var focusAfter = await _editor.HasWidgetFocus();

        // Assert
        AssertEquals(focusBefore, false, "before");
        AssertEquals(focusAfter, true, "after");
    }

    private async Task GetOptions()
    {
        // Arrange

        // Act
        var options = await _editor.GetOptions();
        var option1 = options.Get<int>(EditorOption.foldingMaximumRegions);
        var option2 = options.Get<string>(EditorOption.renderWhitespace);
        var option3 = options.Get<string>(EditorOption.renderFinalNewline);

        // Assert
        AssertNotNull(options);
        AssertEquals(option1, 5000, "foldingMaximumRegions");
        AssertEquals(option2, "selection", "renderWhitespace");
        AssertEquals(option3, "on", "renderFinalNewline");
    }

    private async Task GetOption()
    {
        // Arrange

        // Act
        var option1 = await _editor.GetOption<int>(EditorOption.foldingMaximumRegions);
        var option2 = await _editor.GetOption<string>(EditorOption.renderWhitespace);
        var option3 = await _editor.GetOption<string>(EditorOption.renderFinalNewline);

        // Assert
        AssertEquals(option1, 5000, "foldingMaximumRegions");
        AssertEquals(option2, "selection", "renderWhitespace");
        AssertEquals(option3, "on", "renderFinalNewline");
    }

    private async Task GetRawOptions()
    {
        // Arrange

        // Act
        var rawOptions = await _editor.GetRawOptions();

        // Assert
        AssertNotNull(rawOptions);
        AssertEquals(rawOptions.ScrollBeyondLastLine ?? false, _defaultOptions.ScrollBeyondLastLine ?? false, "scrollBeyondLastLine");
        AssertEquals(rawOptions.RenderFinalNewline ?? "off", _defaultOptions.RenderFinalNewline ?? "off", "renderFinalNewline");
        AssertEquals(rawOptions.SelectOnLineNumbers ?? false, _defaultOptions.SelectOnLineNumbers ?? false, "selectOnLineNumbers");
    }

    private async Task GetValue()
    {
        // Arrange
        await _editor.SetValue(DefaultValue);

        // Act
        var actual = await _editor.GetValue();

        // Assert
        AssertEquals(actual, DefaultValue);
    }

    private async Task SetValue()
    {
        // Arrange
        var expected = "SetValue test value line 1\ntest line 2";

        // Act
        await _editor.SetValue(expected);

        // Assert
        var actual = await _editor.GetValue();
        AssertEquals(actual, expected);
    }

    private async Task GetContentWidth()
    {
        // Arrange
        var expected = 206;
        await _editor.SetValue(DefaultValue);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetContentWidth();

        // Assert
        AssertEquals(actual, expected);
    }

    private async Task GetScrollWidth()
    {
        // Arrange
        await _editor.SetValue(DefaultValue);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetScrollWidth();

        // Assert
        var expected = await _jsRuntime.InvokeAsync<int>("getElementWidth", $"#{_editor.Id} .view-lines");
        AssertEquals(actual, expected);
    }

    private async Task GetScrollLeft()
    {
        // Arrange
        var expected = 50;
        await _editor.SetValue(LoremIpsum);
        await _editor.SetScrollLeft(expected, ScrollType.Immediate);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetScrollLeft();

        // Assert
        AssertEquals(actual, expected);
    }

    private async Task GetContentHeight()
    {
        // Arrange
        var expected = 38;
        await _editor.SetValue(DefaultValue + "\n" + DefaultValue);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetContentHeight();

        // Assert
        AssertEquals(actual, expected);
    }

    private async Task GetScrollHeight()
    {
        // Arrange
        await _editor.SetValue(LoremIpsum);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetScrollHeight();

        // Assert
        var expected = await _jsRuntime.InvokeAsync<int>("getElementHeight", $"#{_editor.Id} .view-lines");
        AssertEquals(actual, expected);
    }

    private async Task GetScrollTop()
    {
        // Arrange
        var expected = 75;
        await _editor.SetValue(LoremIpsum);
        await _editor.SetScrollTop(expected);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var actual = await _editor.GetScrollTop();

        // Assert
        AssertEquals(actual, expected);
    }

    private async Task SetScrollLeft()
    {
        // Arrange
        var expected = 50;
        await _editor.SetValue(LoremIpsum);

        // Act
        await _editor.SetScrollLeft(expected, ScrollType.Immediate);

        // Assert
        await Task.Delay(ValueUpdateDelayMs);
        var actual = await _editor.GetScrollLeft();
        AssertEquals(actual, expected);
    }

    private async Task SetScrollTop()
    {
        // Arrange
        var expected = 75;
        await _editor.SetValue(LoremIpsum);

        // Act
        await _editor.SetScrollTop(expected, ScrollType.Immediate);

        // Assert
        await Task.Delay(ValueUpdateDelayMs);
        var actual = await _editor.GetScrollTop();
        AssertEquals(actual, expected);
    }

    private async Task SetScrollPosition()
    {
        // Arrange
        var expected = new NewScrollPosition
        {
            ScrollLeft = 50,
            ScrollTop = 75
        };
        await _editor.SetValue(LoremIpsum);

        // Act
        await _editor.SetScrollPosition(expected, ScrollType.Immediate);

        // Assert
        await Task.Delay(ValueUpdateDelayMs);
        var actual_top = await _editor.GetScrollTop();
        var actual_left = await _editor.GetScrollLeft();
        AssertEquals(actual_top, expected.ScrollTop);
        AssertEquals(actual_left, expected.ScrollLeft);
    }

    private async Task GetScrolledVisiblePosition()
    {
        // Arrange
        var scroll_position = new NewScrollPosition
        {
            ScrollLeft = 50,
            ScrollTop = 75
        };
        await _editor.SetValue(LoremIpsum);
        await _editor.SetScrollPosition(scroll_position, ScrollType.Immediate);
        await Task.Delay(ValueUpdateDelayMs);

        // Act
        var position = new Position
        {
            LineNumber = 8,
            Column = 15
        };
        var actual = await _editor.GetScrolledVisiblePosition(position);

        // Assert
        AssertEquals(actual.Left, 141);
        AssertEquals(actual.Top, 58);
        AssertEquals(actual.Height, 19);
    }

    private void AssertEquals<T>(T actual, T expected, string testName = "", [CallerMemberName] string caller = "")
        where T : IComparable
    {
        if (string.IsNullOrWhiteSpace(testName))
            testName = caller;
        else
            testName = $"{caller}.{testName}";

        if (!actual.Equals(expected))
            _logger.LogError($"{testName} failed :\n actual is {actual}\n expected is {expected}");
        else
            _logger.LogInformation($"{testName} succeeded :\n actual is {actual}\n expected is {expected}");
    }

    private void AssertNotNull<T>(T value, string testName = "", [CallerMemberName] string caller = "")
    {
        if (string.IsNullOrWhiteSpace(testName))
            testName = caller;
        else
            testName = $"{caller}.{testName}";

        if (value is null)
            _logger.LogError($"{testName} failed :\n value is {null}");
        else
            _logger.LogInformation($"{testName} succeeded :\n value is not null and it's {value}");
    }
}
