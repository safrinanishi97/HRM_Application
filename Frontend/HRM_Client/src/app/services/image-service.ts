// import { Injectable } from '@angular/core';
// import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

// @Injectable({
//   providedIn: 'root'
// })
// export class ImageService {
//    constructor(private sanitizer: DomSanitizer) {}

//   convertByteArrayToImageUrl(byteArray: string | null): SafeUrl | null {
//     if (!byteArray) return null;
//     return this.sanitizer.bypassSecurityTrustUrl(`data:image/jpeg;base64,${byteArray}`);
//   }

//   convertFileToBase64(file: File): Promise<string> {
//     return new Promise((resolve, reject) => {
//       const reader = new FileReader();
//       reader.onload = () => {
//         const base64String = (reader.result as string).split(',')[1];
//         resolve(base64String);
//       };
//       reader.onerror = reject;
//       reader.readAsDataURL(file);
//     });
//   }
// }


import { Injectable } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  constructor(private sanitizer: DomSanitizer) {}

  convertByteArrayToImageUrl(byteArray: string | null, fileExtension?: string): SafeUrl | null {
    if (!byteArray) return null;
    
    const mimeType = this.getMimeTypeFromExtension(fileExtension);
    return this.sanitizer.bypassSecurityTrustUrl(`data:${mimeType};base64,${byteArray}`);
  }

  convertFileToBase64(file: File): Promise<{base64: string, extension: string}> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = (reader.result as string).split(',')[1];
        const extension = this.getFileExtension(file.name);
        resolve({base64: base64String, extension});
      };
      reader.onerror = reject;
      reader.readAsDataURL(file);
    });
  }

  private getMimeTypeFromExtension(extension?: string): string {
    if (!extension) return 'application/octet-stream';
    
    const mimeTypes: {[key: string]: string} = {
      // Images
      'jpg': 'image/jpeg',
      'jpeg': 'image/jpeg',
      'png': 'image/png',
      'gif': 'image/gif',
      'webp': 'image/webp',
      'svg': 'image/svg+xml',
      'bmp': 'image/bmp',
      
      // Documents
      'pdf': 'application/pdf',
      'doc': 'application/msword',
      'docx': 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
      'xls': 'application/vnd.ms-excel',
      'xlsx': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'ppt': 'application/vnd.ms-powerpoint',
      'pptx': 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
      'txt': 'text/plain',
      
      // Archives
      'zip': 'application/zip',
      'rar': 'application/x-rar-compressed',
      
      // Default
      'default': 'application/octet-stream'
    };

    const ext = extension.toLowerCase();
    return mimeTypes[ext] || mimeTypes['default'];
  }

  private getFileExtension(filename: string): string {
    return filename.split('.').pop()?.toLowerCase() || '';
  }
}