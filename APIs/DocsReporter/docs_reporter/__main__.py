from docs_reporter_domain import (
    get_jira_worklogs,
    get_contracts_info,
    get_contract_parties_info,
    get_doc_templates,
    get_template_info,
    format_tex_file_by_doc_template
)


def generate_report_templates():
    jira_worklogs = get_jira_worklogs()
    contracts_info = get_contracts_info()
    contract_parties_info = get_contract_parties_info()
    doc_templates = get_doc_templates()
    infos = map(
        lambda template: get_template_info(
            template, jira_worklogs, contracts_info, contract_parties_info
        ),
        doc_templates,
    )

    for info in infos:
        formatted_tex_file_path = format_tex_file_by_doc_template(info)
        print(formatted_tex_file_path)


if __name__ == "__main__":
    generate_report_templates()
