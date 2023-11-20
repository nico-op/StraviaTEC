import 'dart:io';

abstract class Coordenadas {
  double get latitude;
  double get longitude;
}

class GoogleMapsLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  GoogleMapsLatLng(this.latitude, this.longitude);
}

class LatLongLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  LatLongLatLng(this.latitude, this.longitude);
}

class GPXHelper {
  static const String folderName = 'mi_carpeta';

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
      gpxContent.writeln('    <desc>Details about the waypoint</desc>');
      gpxContent.writeln('  </wpt>');
    }

    gpxContent.writeln('</gpx>');

    return gpxContent.toString();
  }

  Future<void> saveGPXToFile(List<Coordenadas> routePoints,
      {String fileName = 'straviaTEC_route.gpx'}) async {
    String gpxContent = generateGPXContent(routePoints);

    try {
      // Cambia la dirección a la ubicación deseada
      String desiredDirectoryPath =
          'content://com.android.providers.downloads.documents/document/msd%3A20';

      // Construye la ruta completa del archivo GPX en la ubicación deseada
      String filePath = '$desiredDirectoryPath/$fileName';

      // Guarda el archivo GPX en la ubicación deseada
      File file = File(filePath);
      await file.writeAsString(gpxContent);

      print('GPX file saved to: $filePath');
    } catch (e) {
      print('Error saving GPX file: $e');
    }
  }
}
