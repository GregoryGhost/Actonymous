import { JiraCredentials } from "./jira-credentials";
import { MorpherInfo } from "./morpher-info";
import { TemplateBindingsInfo } from "./template-bindings-info";

export interface SettingsExportReport {
    jiraCredentials: JiraCredentials;
    morpher: MorpherInfo;
    templateBindings: TemplateBindingsInfo;
}