export class DocumentsTemplateListOfKeys
{
    id: number;
    name: string;
    keys: DocumentTemplateKeys;
}
class DocumentTemplateKeys {
  id: number;
  keyName: string;
  description: string;
  documentCategoriesId: number;
}