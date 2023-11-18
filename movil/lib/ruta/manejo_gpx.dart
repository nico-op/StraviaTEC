import 'dart:io';
import 'package:file_picker/file_picker.dart';

abstract class Coordenadas {
  double get latitude;
  double get longitude;
}

// LatLng para google_maps_flutter
class GoogleMapsLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  GoogleMapsLatLng(this.latitude, this.longitude);
}

// LatLng para latlong
class LatLongLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  LatLongLatLng(this.latitude, this.longitude);
}

class GPXHelper {
  // Función para crear el contenido GPX
  String generateGPXContent(List<Coordenadas> routePoints) {
    final StringBuffer gpxContent = StringBuffer();

    gpxContent
        .writeln('<?xml version="1.0" encoding="UTF-8" standalone="no" ?>');
    gpxContent.writeln(
        '<gpx version="1.1" xmlns="http://www.topografix.com/GPX/1/1">');

    for (Coordenadas point in routePoints) {
      gpxContent
          .writeln('  <wpt lat="${point.latitude}" lon="${point.longitude}">');
      gpxContent.writeln('    <name>Waypoint</name>');
      gpxContent.writeln('  </wpt>');
    }

    gpxContent.writeln('</gpx>');

    return gpxContent.toString();
  }

  // Función para guardar el contenido GPX en un archivo
  // Función para guardar el contenido GPX en un archivo
  Future<void> saveGPXToFile(List<Coordenadas> routePoints,
      {String fileName = 'straviaTEC_route.gpx'}) async {
    String gpxContent = generateGPXContent(routePoints);

    // Utiliza el paquete file_picker para permitir al usuario elegir la ubicación
    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['gpx'],
    );

    if (result != null && result.files.isNotEmpty) {
      final File file = File(result.files.single.path!);
      await file.writeAsString(gpxContent);

      print('GPX file saved to: ${file.path}');
    } else {
      print('Cancelado por el usuario o no se seleccionó ningún archivo.');
    }
  }

  // Función para cargar el contenido de un archivo GPX (opcional)
  Future<void> loadGPXFromFile() async {
    try {
      FilePickerResult? result = await FilePicker.platform.pickFiles(
        type: FileType.custom,
        allowedExtensions: ['gpx'],
      );

      if (result != null && result.files.isNotEmpty) {
        final File file = File(result.files.single.path!);

        final String gpxContent = await file.readAsString();

        // Aquí puedes procesar el contenido GPX según tus necesidades
        print('Loaded GPX content: $gpxContent');
      } else {
        print('Cancelado por el usuario o no se seleccionó ningún archivo.');
      }
    } catch (e) {
      print('Error loading GPX file: $e');
    }
  }
}
